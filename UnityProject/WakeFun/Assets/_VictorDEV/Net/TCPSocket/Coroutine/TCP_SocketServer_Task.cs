using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Debug = VictorDev.Common.DebugHandler;

namespace VictorDev.Net.TCPSocket
{
    /// <summary>
    /// TCP Socket Server (Instance)
    /// </summary>
    public class TCP_SocketServer_Task
    {
        private TcpListener tcpListener { get; set; }
        public bool isServerRunning { get; private set; } = false;
        private CancellationTokenSource cancellationTokenSource { get; set; }

        /// <summary>
        /// 連線Server資訊
        /// </summary>
        private IPEndPoint serverEndPoint => (IPEndPoint)tcpListener.LocalEndpoint;
        private string serverIP => serverEndPoint.Address.ToString();
        private string serverPort => serverEndPoint.Port.ToString();

        /// <summary>
        /// 儲存連線Client清單 [IP，Client相關資訊]
        /// </summary>
        private Dictionary<string, ClientInfo> clientDict { get; set; } = new Dictionary<string, ClientInfo>();

        private Encoding encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 接收到Client訊息時發送
        /// </summary>
        public Action<string> onReceivedMessageEvent { get; set; }

        /// <summary>
        /// 建立TCP Socket Server
        /// </summary>
        public async Task StartServer(int port)
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, port);
                tcpListener.Start();
                isServerRunning = true;

                Debug.Log($"[ Start Server ] >>> Server started - {serverIP}:{serverPort}. Listening for connections...");
                 
                while (isServerRunning)
                {
                    Debug.Log("等待Client端連線");

                    //等待Client端連線
                    TcpClient client = await tcpListener.AcceptTcpClientAsync();

                    Debug.Log("儲存連線的Client");

                    //儲存連線的Client
                    ClientInfo clientInfo = new ClientInfo(client);
                    clientDict.Add(clientInfo.ipAddress, clientInfo);

                    Debug.Log($">>> Client connected... {clientInfo.ipAddress}:{clientInfo.Port}");

                    //處理接收資訊
                    _ = ReceivedDataHandler(clientInfo);
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[ {isServerRunning} ] Exception: {e.Message}");
            }finally
            {
                StopServer();
            }
        }

        /// <summary>
        /// 處理接收Client資訊
        /// </summary>
        private async Task ReceivedDataHandler(ClientInfo clientInfo)
        {
            try
            {
                NetworkStream stream = clientInfo.client.GetStream();

                byte[] buffer = new byte[1024];
                int bytesRead;
                // 讀取傳送進來的資料
                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string receivedData = encoding.GetString(buffer, 0, bytesRead);
                    clientInfo.SetReceivedData(receivedData);
                    onReceivedMessageEvent?.Invoke(receivedData);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error handling client: {e.Message}");
            }
        }

        /// <summary>
        /// 傳送訊息給指定Client
        /// </summary>
        public async Task SendMessageToClient(string ipAddress, string message)
        {
            try
            {
                NetworkStream stream = clientDict[ipAddress].client.GetStream();
                byte[] data = encoding.GetBytes(message);
                await stream.WriteAsync(data, 0, data.Length);
            }
            catch (Exception e)
            {
                Debug.LogError($"[ SendMessageClient ] >>> Error : {e.Message}");
            }
        }

        /// <summary>
        /// 全域廣播訊息
        /// </summary>
        public async Task BroadcastToAllClients(string message)
        {
            foreach (string ipAddress in clientDict.Keys)
            {
                await SendMessageToClient(ipAddress, message);
            }
        }

        /// <summary>
        /// 指定Client IP斷線
        /// </summary>
        public void DisconnectClient(string ipAddress)
        {
            _ = SendMessageToClient(ipAddress, "Server Disconnect....");
            clientDict[ipAddress].Disconnect();
            clientDict.Remove(ipAddress);
        }
        /// <summary>
        /// 全部Client斷線
        /// </summary>
        public void DisconnectAllClient()
        {
            foreach (string ipAddress in clientDict.Keys)
            {
                DisconnectClient(ipAddress);
            }
        }

        /// <summary>
        /// 關閉Server 
        /// </summary> 
        public void StopServer()
        {
            if (isServerRunning)
            {
                isServerRunning = false;
                tcpListener.Stop();
                Debug.Log(">>> Server stopped.");
            }
        }


        /// <summary>
        /// 連線Client資訊
        /// </summary>
        private class ClientInfo
        {
            public TcpClient client { get; private set; }
            private IPEndPoint clientEndPoint { get; set; }

            public string ipAddress => clientEndPoint.Address.ToString();
            public string Port => clientEndPoint.Port.ToString();

            /// <summary>
            /// 接收訊息之歷史記錄
            /// </summary>
            public Dictionary<DateTime, string> receiveDataHistory { get; private set; }

            public ClientInfo(TcpClient client)
            {
                this.client = client;
                clientEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
            }

            public void SetReceivedData(string receivedData)
            {
                DateTime receivedDateTime = DateTime.Now;
                receiveDataHistory ??= new Dictionary<DateTime, string>();
                receiveDataHistory[receivedDateTime] = receivedData;
                Debug.Log($"[ {receivedDateTime.ToLongTimeString()} ] >>> Received data from {ipAddress}: {receivedData}");
            }

            public void Disconnect()
            {
                client.Close();
                receiveDataHistory?.Clear();

                Debug.Log($"[ {DateTime.Now.ToLongTimeString()} ] >>> {ipAddress} Disconnect...");
            }
        }
    }
}
