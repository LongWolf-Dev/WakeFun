using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using VictorDev.Common;
using VictorDev.EditorTool;
using VictorDev.Net.TCPSocket;

public class ClientTest : MonoBehaviour
{
    private TCP_SocketClient_Thread tcpClient;

    [SerializeField] private TMP_InputField inputIP, inputPort;
    [SerializeField] private Button btnConnect, btnDisconnect;
    [SerializeField] private InputField inputMessage;
    [SerializeField] private Button btnSend;
    [SerializeField] private Text console;

    private void Start()
    {
        console.text = "";

        btnDisconnect.gameObject.SetActive(false);
        btnConnect.onClick.AddListener(() =>
        {
            btnConnect.gameObject.SetActive(false);
            btnDisconnect.gameObject.SetActive(true);
            ConnectToServer();
        });
        btnDisconnect.onClick.AddListener(() =>
        {
            btnDisconnect.gameObject.SetActive(false);
            btnConnect.gameObject.SetActive(true);
            Disconnected();
        });

        btnSend.onClick.AddListener(
            () =>
            {
                SendMessageToServer(inputMessage.text);
                inputMessage.text = "";
            }
            );

        DebugHandler.onLogEvent += (logType, msg) =>
         UnityMainThreadDispatcher.Enqueue(() => PrintMessageToConsole(msg));
    } 

    public void ConnectToServer()
    {
        tcpClient ??= new TCP_SocketClient_Thread();
        tcpClient.ConnectToTcpServer(inputIP.text, int.Parse(inputPort.text));
        tcpClient.onReceivedMsgFromServerEvent += (serverEndPoint, msg) => PrintMessageToConsole(msg);
    }

    private void PrintMessageToConsole(string message) => console.text += $"\n{message}";

    public void Disconnected() { tcpClient?.Disconnected(); }

    private void SendMessageToServer(string sourceText) => tcpClient.SendMessage(sourceText);

    private void OnDestroy() => Disconnected();

#if UNITY_EDITOR
    [CustomEditor(typeof(ClientTest))]
    private class Inspector : InspectorEditor<ClientTest>
    {
        private string sourceText;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUIStyle gUIStyle = _CreateButtonStyle();

            _CreateButton("Start Server", gUIStyle, () => { instance.ConnectToServer(); });
            _CreateButton("Stop Server", gUIStyle, () => { instance.Disconnected(); });

            _CreateSpacer();

            _CreateTextFiled("Sending Message", ref sourceText);
            _CreateButton("Send Message", gUIStyle, () => { instance.SendMessageToServer(sourceText); });
        }
    }
#endif
}
