#if false

using System;
using System.Collections.Generic;

namespace VictorDev.Net.SignalrUtils
{
    /// <summary>
    /// SignalR Client連線管理器
    /// <para>參照：https://github.com/evanlindsey/Unity-WebGL-SignalR/tree/main?tab=readme-ov-file</para>
    /// </summary>
    public abstract class SignalrClientHandler
    {
        private static SignalR_Extended signalR { get; set; }

        /// <summary>
        /// 初始化，設置各個CallBack
        /// </summary>
        /// <param name="onConnectionStarted">建立連線時</param>
        /// <param name="onConnectionClosed">結束連線時</param>
        /// <param name="onConnectionFailed">連線失敗時</param>
        /// <param name="channelList">Channel監聽CallBack</param>
        public static void Init(string signalrURL
            , EventHandler<ConnectionEventData> onConnectionStarted, EventHandler<ConnectionEventData> onConnectionClosed
            , Action<string> onConnectionFailed
            , List<SignalrChannelData> channelList)
        {
            signalR = new SignalR_Extended(); //每次呼叫用new，避免Task衝突而產生LogError
            signalR.Init(signalrURL);
            signalR.ConnectionStarted += onConnectionStarted;
            signalR.ConnectionClosed += onConnectionClosed;
            signalR.ConnectionFailed += onConnectionFailed;
            channelList.ForEach(channelData => signalR.On(channelData.channelName, channelData.onReceivedMessage));
        }

        /// <summary>
        /// 結束連線
        /// </summary>
        public static void Shutdown() => signalR.Stop();

        /// <summary>
        /// 進行連線
        /// </summary>
        public static void Connect() => signalR.Connect();

        /// <summary>
        /// 傳送訊息至指定通道名稱
        /// <para>+ params可以多筆資料，後面型態要加[]；object 不限型態</para>
        /// </summary>
        /// <param name="channelName">通道名稱</param>
        public static void Send(string channelName, params object[] data) => signalR.Invoke(channelName, data);
    }

    public struct SignalrChannelData
    {
        /// <summary>
        /// 通道名稱
        /// </summary>
        public string channelName;
        /// <summary>
        /// 收到訊息之CallBack
        /// </summary>
        public Action<string> onReceivedMessage;

        public SignalrChannelData(string channelName, Action<string> callBack)
        {
            this.channelName = channelName;
            this.onReceivedMessage = callBack;
        }
    }
}

#endif