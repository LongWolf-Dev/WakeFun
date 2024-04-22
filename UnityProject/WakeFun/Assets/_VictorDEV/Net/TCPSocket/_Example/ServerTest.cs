using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using VictorDev.Common;
using VictorDev.EditorTool;
using VictorDev.Net.TCPSocket;

public class ServerTest : MonoBehaviour
{
    private TCP_SocketServer_Thread tcpServer;

    [SerializeField] private TMP_InputField inputPort;
    [SerializeField] private Button btnStartServer, btnStopServer;
    [SerializeField] private InputField inputMessage;
    [SerializeField] private Button btnSend;
    [SerializeField] private Text console;

    private void Start()
    {
        console.text = "";

        btnStopServer.gameObject.SetActive(false);
        btnStartServer.onClick.AddListener(() =>
        {
            btnStartServer.gameObject.SetActive(false);
            btnStopServer.gameObject.SetActive(true);
            StartServer();
        });
        btnStopServer.onClick.AddListener(() =>
        {
            btnStopServer.gameObject.SetActive(false);
            btnStartServer.gameObject.SetActive(true);
            StopServer();
        });

        btnSend.onClick.AddListener(
            () =>
            {
                SendMessageToClient(inputMessage.text);
                inputMessage.text = "";
            }
            );

        DebugHandler.onLogEvent += (logType, msg) =>
         UnityMainThreadDispatcher.Enqueue(() => PrintMessageToConsole(msg));
    }

    public void StartServer()
    {
        tcpServer ??= new TCP_SocketServer_Thread();
        tcpServer.StartServer(int.Parse(inputPort.text));
        tcpServer.onReceivedMsgFromClientEvent += (clientEndPoint, msg) => PrintMessageToConsole(msg);
    }

    //private void PrintMessageToConsole(string message) => console.text += $"\n{message}";
    private void PrintMessageToConsole(string message) { }
    public void StopServer() { tcpServer?.StopServer(); }

    private void SendMessageToClient(string sourceText) => tcpServer.SendMessage(sourceText);

    private void OnApplicationQuit() => StopServer();

#if UNITY_EDITOR
[CustomEditor(typeof(ServerTest))]
    private class Inspector : InspectorEditor<ServerTest>
    {
        private string sourceText;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUIStyle gUIStyle = _CreateButtonStyle();

            _CreateButton("Start Server", gUIStyle, () => { instance.StartServer(); });
            _CreateButton("Stop Server", gUIStyle, () => { instance.StopServer(); });

            _CreateSpacer();

            _CreateTextFiled("Sending Message", ref sourceText);
            _CreateButton("Send Message", gUIStyle, () => { instance.SendMessageToClient(sourceText); });
        }
    }
#endif
}
