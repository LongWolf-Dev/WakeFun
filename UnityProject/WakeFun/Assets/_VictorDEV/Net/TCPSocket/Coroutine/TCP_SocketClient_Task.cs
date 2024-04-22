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
    /// TCP Socket Client端 (instance)
    /// </summary>
    public class TCP_SocketClient_Task
    {
        #region [ >>> Variables ]
        private TcpClient client;
        private NetworkStream stream;

        /// <summary>
        /// 連線Server資訊
        /// </summary>
        private IPEndPoint serverEndPoint { get; set; }
        private string serverIP => serverEndPoint.Address.ToString();
        private string serverPort => serverEndPoint.Port.ToString();
        /// <summary>
        /// 本地Client資訊
        /// </summary>
        private IPEndPoint localEndPoint { get; set; }
        private string clientIP => localEndPoint.Address.ToString();
        private string clientPort => localEndPoint.Port.ToString();

        /// <summary>
        /// 資料編碼格式
        /// </summary>
        private Encoding encoding { get; set; } = Encoding.UTF8;

        public Action<string> onReceivedMessage { get; set; }

        /// <summary>
        /// 傳送訊息歷史記錄 [時間點，訊息]
        /// </summary>
        private Dictionary<DateTime, string> messageHistory { get; set; } = new Dictionary<DateTime, string>();

        private CancellationTokenSource cancellationTokenSource;
        #endregion

        /// <summary>
        /// 連線至TCP Socket Server
        /// </summary>
        public async Task ConnectToServer(string serverIP, int serverPort)
        {
            Task task = Task.Run(() =>
            {
                // 建立一個 TcpClient 對象並連接到伺服器
                using (client = new TcpClient(serverIP, serverPort))
                {
                    cancellationTokenSource = new CancellationTokenSource();

                    stream = client.GetStream();

                    serverEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;
                    localEndPoint = (IPEndPoint)client.Client.LocalEndPoint;

                    Debug.Log($"[ ConnectToServer ] >>> Connected to server - {serverIP}:{serverPort}");
                    Debug.Log($"[ ConnectToServer ] >>> Local client - {clientIP}:{clientPort}");

                    _ = ReceiveDataAsync(cancellationTokenSource.Token);
                }
            });

            try
            {
                await task;
            }
            catch (Exception e)
            {
                Debug.LogError($"[ ConnectToServer - {serverIP}:{serverPort} ] >>> Failed to connect to server: {e.Message}");
            }
        }

        private async Task ReceiveDataAsync(CancellationToken cancellationToken)
        {
            byte[] buffer = new byte[1024]; // Adjust buffer size as needed
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                    if (bytesRead > 0)
                    {
                        string receivedData = encoding.GetString(buffer, 0, bytesRead);
                        onReceivedMessage?.Invoke(receivedData);
                        Debug.Log($"Received data: {receivedData}");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error receiving data: {e.Message}");
                    break;
                }
            }
        }

        /// <summary>
        /// 向伺服器發送數據
        /// </summary>
        public async Task SendMessageToServer(string message)
        {
            try
            {
                await Task.Run(() =>
                {
                    // 將消息轉換為字節數組
                    byte[] data = encoding.GetBytes(message);
                    // 向伺服器發送數據
                    stream.Write(data, 0, data.Length);
                    messageHistory[DateTime.Now] = message;
                    Debug.Log($"[ {DateTime.Now} ] >>> SendMessageToServer {serverIP}:{serverPort} - " + message);
                });
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to send message to server: " + e.Message);
            }
        }

        /// <summary>
        /// 中斷連線
        /// </summary>
        public void Disconnect()
        {
            _ = SendMessageToServer("Client Request Disconnecting....");
            // 關閉連接
            stream?.Close();
            client?.Close();
            messageHistory.Clear();
        }
    }
}
