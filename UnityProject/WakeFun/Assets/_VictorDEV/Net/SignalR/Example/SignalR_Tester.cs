#if false

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VictorDev.Common;
using VictorDev.Net.SignalrUtils;

public class SignalR_Tester : MonoBehaviour
{
    [SerializeField] private List<string> signalrURL;
    [SerializeField] private List<string> signalrChannelName;
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private Button btnConnect;

    [SerializeField] private Text console;

    private string signalrPath { get; set; }

    private List<SignalrChannelData> channelList { get; set; }

    private void Start()
    {
        //設置DropDown項目集
        List<Dropdown.OptionData> optionList = new List<Dropdown.OptionData>();
        signalrURL.ForEach(url =>
        {
            optionList.Add(new Dropdown.OptionData(url));
        });
        dropdown.options = optionList;

        btnConnect.onClick.AddListener(Connect);

        console.text = "";

        channelList = new List<SignalrChannelData>();
        signalrChannelName.ForEach(channel =>
        {
            channelList.Add(new SignalrChannelData(channel,
                (msg) =>
                {
                    msg = $"\t[{DateTime.Now.ToLongTimeString()}] Receive from Channel < {channel} > :::: {msg}";
                    Debug.Log(msg);
                    PrintToConsole(msg);
                }
            ));
        });
    }
    private void Connect()
    {
        signalrPath = dropdown.options[dropdown.value].text;

        PrintToConsole($">>> Conntecting... {signalrPath}");
        SignalrClientHandler.Init(signalrPath, onConnectionStarted, onConnectionClosed, onConnectionFailed, channelList);

        SignalrClientHandler.Connect();
    }

    private void onConnectionFailed(string msg)
    {
        PrintToConsole($">>> {msg}");
    }

    private void onConnectionStarted(object sender, ConnectionEventData e)
    {
        PrintToConsole($">>> ConnectionStarted: [{e.ConnectionId}]");
    }

    private void onConnectionClosed(object sender, ConnectionEventData e)
    {
        PrintToConsole($">>> ConnectionClosed: [{e.ConnectionId}]");
    }

    private void PrintToConsole(string msg)
    {
        Action action = () => console.text += $"{msg}\n";
        UnityMainThreadDispatcher.Enqueue(action);
    }
    private void OnDestroy()
    {
        SignalrClientHandler.Shutdown();
    }

}
#endif