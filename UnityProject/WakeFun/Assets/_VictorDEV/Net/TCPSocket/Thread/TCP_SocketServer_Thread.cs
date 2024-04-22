using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Debug = VictorDev.Common.DebugHandler;

namespace VictorDev.Net.TCPSocket
{
    /// <summary>
    /// TCP Socket Server (Instance)
    /// <para>https://gist.github.com/danielbierwirth/0636650b005834204cb19ef5ae6ccedb</para>
    /// </summary>
    public class TCP_SocketServer_Thread
    {
        #region [ >>>Private variables ]
        private Thread serverThread { get; set; }
        public bool isServerRunning { get; private set; } = false;
        private TcpListener tcpListener { get; set; }
        private IPEndPoint serverEndPoint => (IPEndPoint)tcpListener.LocalEndpoint;
        private string serverIP => serverEndPoint.Address.ToString();
        private string serverPort => serverEndPoint.Port.ToString();

        private TcpClient connectedTcpClient { get; set; }
        private IPEndPoint clientEndPoint => (IPEndPoint)connectedTcpClient.Client.RemoteEndPoint;
        private string clientIP => clientEndPoint.Address.ToString();
        private string clientPort => clientEndPoint.Port.ToString();
        #endregion

        /// <summary>
        /// 資料編碼格式
        /// </summary>
        private Encoding encoding = Encoding.UTF8;

        public Action<IPEndPoint, string> onReceivedMsgFromClientEvent { get; set; }
        public Action<IPEndPoint, string> onSendMsgToClientEvent { get; set; }
        public Action<IPEndPoint> onServerStarted { get; set; }
        public Action<IPEndPoint> onClientConnected { get; set; }
        public Action<IPEndPoint> onServerStopEvent { get; set; }

        /// <summary>
        /// 建立TCP Server
        /// </summary>
        public void StartServer(int port)
        {
            if (isServerRunning)
            {
                Debug.Log($">>> Server is already running. [{serverIP}:{serverPort}]");
                return;
            }

            // Start TcpServer background thread 		
            serverThread = new Thread(new ThreadStart(() => ListenForIncommingRequests(port)));
            serverThread.IsBackground = true;
            serverThread.Start();
        }
        /// <summary> 	
        /// Runs in background TcpServerThread; Handles incomming TcpClient requests 	
        /// </summary> 	
        private void ListenForIncommingRequests(int port)
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
                tcpListener.Start();

                isServerRunning = true;
                // [發送事件]
                onServerStarted?.Invoke(serverEndPoint);

                Debug.Log($">>> TCP Server Started. [{serverIP}:{serverPort}]");

                while (isServerRunning)
                {
                    ReadMessageFromClient();
                }
            }
            catch (SocketException socketException)
            {
                Debug.Log(">>> SocketException " + socketException.ToString());
            }
        }

        /// <summary>
        /// 讀取從Client傳來的資料
        /// </summary>
        private void ReadMessageFromClient()
        {
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {
                    // [發送事件]
                    onClientConnected?.Invoke(clientEndPoint);

                    // Get a stream object for reading 					
                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {
                        int length;
                        // Read incomming stream into byte arrary. 						
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            // Convert byte array to string message. 							
                            string clientMessage = encoding.GetString(incommingData);

                            // [發送事件]
                            onReceivedMsgFromClientEvent?.Invoke(clientEndPoint, clientMessage);

                            Debug.Log($">>> Received Message From Client [{clientIP}:{clientPort}]: {clientMessage}");
                        }
                    }
                }
            }
        }

        /// <summary> 	
        /// 發送訊息給Client
        /// </summary> 	
        public void SendMessage(string msg)
        {
            if (connectedTcpClient == null)
            {
                Debug.Log(">>> [SendMessage] There is no client conntected.");
                return;
            }
            try
            {
                // Get a stream object for writing. 			
                NetworkStream stream = connectedTcpClient.GetStream();
                if (stream.CanWrite)
                {
                    // Convert string message to byte array.                 
                    byte[] serverMessageAsByteArray = encoding.GetBytes(msg);
                    // Write byte array to socketConnection stream.               
                    stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);

                    // [發送事件]
                    onSendMsgToClientEvent?.Invoke(serverEndPoint, msg);

                    Debug.Log($">>> Send Message to Client [{clientIP}:{clientPort}]: {msg}");
                }
            }
            catch (SocketException socketException)
            {
                Debug.Log(">>>[SendMessage] Socket exception: " + socketException);
            }
        }

        /// <summary>
        /// 關閉TCP Socket Server
        /// </summary>
        public void StopServer()
        {
            Debug.Log(">>> Server Shutdown.");

            tcpListener.Stop();
            isServerRunning = false;

            if (serverThread != null)
            {
                if (serverThread.IsAlive) serverThread.Abort();
            }

            // [發送事件]
            onServerStopEvent?.Invoke(serverEndPoint);
        }
    }
}