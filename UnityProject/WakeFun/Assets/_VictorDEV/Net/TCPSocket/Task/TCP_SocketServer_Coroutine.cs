using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using Debug = VictorDev.Common.DebugHandler;

namespace VictorDev.Net.TCPSocket
{
    /// <summary>
    /// TCP Socket Server (Instance)
    /// <para>即時偵測Client是否連線中：https://blog.darkthread.net/blog/detected-tcpclient-connection-status/</para>
    /// </summary>
    public class TCP_SocketServer_Coroutine : MonoBehaviour
    {
        #region [ >>>Private variables ]
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
        /// 消息Buffer大小
        /// </summary>
        private int bufferSize = 1024;

        /// <summary>
        /// 資料編碼格式
        /// </summary>
        private Encoding encoding = Encoding.UTF8;

        /// <summary>
        /// 客戶端名單
        /// </summary>
        public List<TcpClient> tcpClientList { get; private set; } = new List<TcpClient>();

        public Action<TcpClient, string> onReceivedMsgFromClientEvent { get; set; }
        public Action<TcpClient, string> onSendMsgToClientEvent { get; set; }
        public Action<TcpListener> onServerStarted { get; set; }
        public Action<TcpClient> onClientConnected { get; set; }
        public Action<TcpClient> onClientListUpdate { get; set; }
        public Action<TcpListener> onServerStopEvent { get; set; }

        private Coroutine listenForClients { get; set; }
        private Coroutine readMessageFromClient { get; set; }

        /// <summary>
        /// 建立TCP Socket Server
        /// </summary>
        public void StartServer(int port)
        {
            if (isServerRunning)
            {
                Debug.Log($">>> Server is already running. [{serverIP}:{serverPort}]");
                return;
            }

            try
            {
                tcpListener = new TcpListener(IPAddress.Any, port);
                tcpListener.Start();
                isServerRunning = true;
                Debug.Log($">>> TCP Server Started. [{serverIP}:{serverPort}]");

                // [發送事件]
                onServerStarted?.Invoke(tcpListener);

                // 启动Coroutine来处理客户端连接和消息
                listenForClients = StartCoroutine(ListenForClients());
            }
            catch (Exception e)
            {
                Debug.LogWarning("[StartServer] >>> Exception: " + e.Message);
            }
        }

        /// <summary> 	
        /// 等待Client端連線進來
        /// </summary> 	
        private IEnumerator ListenForClients()
        {
            while (isServerRunning)
            {
                if (tcpListener.Pending())
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    tcpClientList.Add(client);
                    // [發送事件]
                    onClientConnected?.Invoke(client);
                    // 在另一个Coroutine中处理客户端消息
                    readMessageFromClient = StartCoroutine(ReadMessageFromClient(client));
                    Debug.Log(">>> Client connected from: " + client.Client.RemoteEndPoint);
                }
                yield return null; // 让出控制权，等待下一帧
            }
        }

        /// <summary>
        /// 讀取從Client傳來的資料
        /// </summary>
        private IEnumerator ReadMessageFromClient(TcpClient tcpClient)
        {
            NetworkStream stream = tcpClient.GetStream();
            Byte[] bytes = new Byte[bufferSize];
            while (tcpClient.Client.Connected) //檢查目前是否連線中
            {
                if (stream.DataAvailable)
                {
                    int bytesRead = stream.Read(bytes, 0, bytes.Length);
                    if (bytesRead > 0)
                    {
                        string clientMessage = encoding.GetString(bytes, 0, bytesRead);
                        Debug.Log($">>> Received Message From Client [{clientIP}:{clientPort}]: {clientMessage}");

                        // [發送事件]
                        onReceivedMsgFromClientEvent?.Invoke(tcpClient, clientMessage);
                    }
                }
                yield return null; // 让出控制权，等待下一帧
            }
        }

        /// <summary> 	
        /// 發送訊息給Client
        /// </summary> 	
        public void SendMessage(TcpClient tcpClient, string msg)
        {
            if (tcpClient.Connected == false)
            {
                Debug.Log($">>> [SendMessage] tcpClient is NOT conntected. [{tcpClient.Client.RemoteEndPoint}]");
                return;
            }

            NetworkStream stream = tcpClient.GetStream();
            byte[] replyBuffer = encoding.GetBytes(msg);
            try
            {
                stream.Write(replyBuffer, 0, replyBuffer.Length);
                // [發送事件]
                onSendMsgToClientEvent?.Invoke(tcpClient, msg);
                Debug.Log($">>> Send Message to Client [{clientIP}:{clientPort}]: {msg}");
            }
            catch (Exception ex)
            {
                Debug.Log(">>>[SendMessage] Exception: " + ex);
            }
        }

        /// <summary>
        /// 關閉TCP Socket Server
        /// </summary>
        private void StopServer()
        {
            isServerRunning = false;

            if (listenForClients != null) StopCoroutine(listenForClients);
            if (readMessageFromClient != null) StopCoroutine(readMessageFromClient);

            tcpListener?.Stop();
            tcpClientList.Clear();

            // [發送事件]
            onServerStopEvent?.Invoke(tcpListener);
        }

        /// <summary>
        /// 中斷指定客戶端的連線
        /// </summary>
        public void DisconnectClient(TcpClient client)
        {
            if (client != null)
            {
                if (client.Connected) //僅顯示是否連線過，不代表目前連線狀況
                {
                    try
                    {
                        StopCoroutine(ReadMessageFromClient(client));
                        client.Close(); //之後client.Connected不會立即變成false

                        tcpClientList.Remove(client);

                        Debug.Log($">>> Disconnecting client: {client.Client.RemoteEndPoint}");
                    }
                    catch (Exception ex)
                    {
                        Debug.Log(">>>[DisconnectClient] Exception: " + ex);
                    }
                }
            }
        }

        private void OnApplicationQuit() => StopServer();
        private void OnDestroy() => StopServer();
    }
}

/*
 * 
 *用Poll方式檢查client目前連線狀態
 private bool IsClientConnected(TcpClient client)
{
    if (client == null)
    {
        return false;
    }

    try
    {
        // 使用 Poll() 方法检查套接字状态
        if (client.Client.Poll(0, SelectMode.SelectRead))
        {
            // 如果 Poll() 返回 true 并且可读，则说明连接仍然有效
            return true;
        }

        // 否则检查套接字的错误状态以确认连接是否已断开
        byte[] buffer = new byte[1];
        if (client.Client.Receive(buffer, SocketFlags.Peek) == 0)
        {
            // 如果 Receive() 返回 0，则说明连接已断开
            return false;
        }

        return true;
    }
    catch
    {
        // 发生异常时，认为连接已断开
        return false;
    }
} 
 * 
 *
 *檢查心跳包
private IEnumerator CheckHeartbeat()
    {
        while (true)
        {
            if (client != null && client.Connected)
            {
                // 设置超时时间为2秒，检查是否收到心跳包
                client.Client.ReceiveTimeout = 2000;

                try
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = client.Client.Receive(buffer);
                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    if (message == "Heartbeat")
                    {
                        Debug.Log("Received heartbeat from client.");
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("Connection lost: " + e.Message);
                    client.Close();
                    break;
                }
            }

            yield return new WaitForSeconds(1.0f); // 每隔1秒检查一次
        }
    } 
*/