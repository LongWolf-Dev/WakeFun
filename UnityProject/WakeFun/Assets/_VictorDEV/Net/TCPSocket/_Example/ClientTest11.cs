using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using VictorDev.EditorTool;
using VictorDev.Net.TCPSocket;

public class ClientTest11 : MonoBehaviour
{
    private TCP_SocketClient_Task tcpClient;
    [SerializeField] private TMP_InputField inputIP;
    [SerializeField] private TMP_InputField inputPort;
    [SerializeField] private Button btnConnect;
    [SerializeField] private TMP_InputField inputSendMessage;
    [SerializeField] private Button btnSendMessage;
    [SerializeField] private TextMeshPro console;

    private void Start()
    {
        btnConnect.onClick.AddListener(ConnectToServer);
        btnSendMessage.onClick.AddListener(() => SendMessageToServer(inputSendMessage.text));
    }
    public void ConnectToServer()
    {
        tcpClient = new TCP_SocketClient_Task();
        _ = tcpClient.ConnectToServer(inputIP.text, int.Parse(inputPort.text));
        
    }

    public void Disconnect() { tcpClient.Disconnect(); }

    private void SendMessageToServer(string sourceText) => _ = tcpClient.SendMessageToServer(sourceText);

    [ContextMenu("Stop")]
    private void OnDestroy() => Disconnect();

#if UNITY_EDITOR
    [CustomEditor(typeof(ClientTest11))]
    private class Inspector : InspectorEditor<ClientTest11>
    {
        private string sourceText;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUIStyle gUIStyle = _CreateButtonStyle();

            _CreateButton("ConnectToServer", gUIStyle, () => { instance.ConnectToServer(); });
            _CreateButton("Disconnect", gUIStyle, () => { instance.Disconnect(); });

            _CreateSpacer();

            _CreateTextFiled("Sending Message", ref sourceText);
            _CreateButton("Send Message", gUIStyle, () => { instance.SendMessageToServer(sourceText); });
        }
    }
#endif
}
