using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Debug = VictorDev.Common.DebugHandler;
namespace VictorDev.Net.TCPSocket
{
    /// <summary>
    /// TCP Socket Client (Instance)
    /// </summary>
    public class TCP_SocketClient_Thread
    {
        #region [ >>> Private variables ]
        private Thread clientReceiveThread { get; set; }
        public bool isServerConnected { get; private set; } = false;
        private TcpClient tcpClient { get; set; }

        private IPEndPoint serverEndPoint => (IPEndPoint)tcpClient.Client.RemoteEndPoint;
        private string serverIP => serverEndPoint.Address.ToString();
        private string serverPort => serverEndPoint.Port.ToString();
        private IPEndPoint clientEndPoint => (IPEndPoint)tcpClient.Client.LocalEndPoint;
        private string clientIP => clientEndPoint.Address.ToString();
        private string clientPort => clientEndPoint.Port.ToString();
        #endregion

        /// <summary>
        /// 資料編碼格式
        /// </summary>
        private Encoding encoding = Encoding.UTF8;

        public Action<IPEndPoint, string> onReceivedMsgFromServerEvent { get; set; }
        public Action<IPEndPoint, string> onSendMsgToServerEvent { get; set; }
        public Action<IPEndPoint> onServerConntected { get; set; }
        public Action<IPEndPoint> onClientDisconnectedEvent { get; set; }

        /// <summary> 	
        /// 連線至TCP Server
        /// </summary> 	
        public void ConnectToTcpServer(string ipAddress, int port)
        {

            if (isServerConnected)
            {
                Debug.Log($">>> Server is already connected. [{serverIP}:{serverPort}]");
                return;
            }

            try
            {
                clientReceiveThread = new Thread(new ThreadStart(() => ListenForData(ipAddress, port)));
                clientReceiveThread.IsBackground = true;
                clientReceiveThread.Start();
            }
            catch (Exception e)
            {
                Debug.Log("On client connect exception: " + e);
            }
        }
        /// <summary> 	
        /// Runs in background clientReceiveThread; Listens for incomming data. 	
        /// </summary>     
        private void ListenForData(string ipAddress, int port)
        {
            try
            {
                tcpClient = new TcpClient(ipAddress, port);

                isServerConnected = true;
                // [發送事件]
                onServerConntected?.Invoke(serverEndPoint);

                Debug.Log($">>> TCP Server Connected. [{serverIP}:{serverPort}]");
                Debug.Log($">>> Local IP : [{clientIP}:{clientPort}]");

                ReadMessageFromServer();
            }
            catch (SocketException socketException)
            {
                Debug.LogWarning(">>> Socket exception: " + socketException);
            }
        }
        /// <summary>
        /// 讀取從Server傳來的資料
        /// </summary>
        private void ReadMessageFromServer()
        {
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    int length;
                    // Read incomming stream into byte arrary. 					
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        var incommingData = new byte[length];
                        Array.Copy(bytes, 0, incommingData, 0, length);
                        // Convert byte array to string message. 						
                        string serverMessage = encoding.GetString(incommingData);
                        // [發送事件]
                        onReceivedMsgFromServerEvent?.Invoke(serverEndPoint, serverMessage);

                        Debug.Log($">>> Received Message From Server [{serverIP}:{serverPort}]: {serverMessage}");
                    }
                }
            }
        }

        /// <summary> 	
        /// 發送訊息給Server
        /// </summary> 	
        public void SendMessage(string msg)
        {
            if (tcpClient == null)
            {
                Debug.Log(">>> [SendMessage] There is no server conntected.");
                return;
            }
            try
            {
                // Get a stream object for writing. 			
                NetworkStream stream = tcpClient.GetStream();
                if (stream.CanWrite)
                {
                    // Convert string message to byte array.                 
                    byte[] clientMessageAsByteArray = encoding.GetBytes(msg);
                    // Write byte array to socketConnection stream.                 
                    stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                    // [發送事件]
                    onSendMsgToServerEvent?.Invoke(serverEndPoint, msg);

                    Debug.Log($">>> Send Message to Server [{serverIP}:{serverPort}]: {msg}");
                }
            }
            catch (SocketException socketException)
            {
                Debug.Log(">>>[SendMessage] Socket exception: " + socketException);
            }
        }

        /// <summary>
        /// 與Server斷線
        /// </summary>
        public void Disconnected()
        { 
            tcpClient?.Close();
            Debug.Log(">>> Disconnect with Server...");
            isServerConnected = false;
            if (clientReceiveThread != null && clientReceiveThread.IsAlive) clientReceiveThread.Abort();

            // [發送事件]
            onClientDisconnectedEvent?.Invoke(serverEndPoint);
        }
    }
}