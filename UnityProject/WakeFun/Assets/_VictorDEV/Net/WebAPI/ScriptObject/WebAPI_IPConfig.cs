using UnityEngine;

namespace VictorDev.Net.WebAPI
{
    /// <summary>
    /// 設定WebAPI的IP與PORT
    /// </summary>
    [CreateAssetMenu(fileName = "★ WebAPI連線IP設定", menuName = ">>VictorDev<</Net/WebAPI/IPConfig-  網址與埠號設定")]
    public class WebAPI_IPConfig : ScriptableObject
    {
        [Header(">>> WebAPI Server的IP")]
        [SerializeField] private string ip;

        [Header(">>> WebAPI Server的Port")]
        [SerializeField, Range(0, 99999)] private int port = 8080;

        public string WebIP_Port => $"{ip}:{port.ToString()}";
    }
}