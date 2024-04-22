using System;
using UnityEngine;

namespace VictorDev.Net.WebAPI
{
    /// <summary>
    /// 呼叫WebAPI的封包格式(ScriptableObject)
    /// </summary>
    [CreateAssetMenu(fileName = "WebAPI網路請求包", menuName = ">>VictorDev<</Net/WebAPI/RequestPackage - 網路請求設定")]
    public class WebAPI_RequestPackage : ScriptableObject
    {
        [Header(">>> 設定WebAPI的IP與PORT (選填)")]
        [SerializeField] private WebAPI_IPConfig ipConfig;

        /// <summary>
        /// 設定WebAPI的IP與PORT (可選)
        /// </summary>
        [Header(">>> WebAP完整路徑 / WebAPI IP之後的路徑")]
        [TextArea(1, 3)][SerializeField] private string apiURL;

        [Header(">>> GET方法之後的變數 (選填)")]
        public string urlGetVariables;

        [Space(20)]

        public RequestMethod method;
        public Authorization authorization;

        [SerializeField] private AccessToken accessToken;
        [SerializeField] private JsonString jsonString;
        public WebAPI_RequestPackage(string url) => this.apiURL = url;

        private UriBuilder uriBuilder;

        /// <summary>
        /// 完整API網址
        /// </summary>
        public string url
        {
            get
            {
                if (ipConfig != null)
                {
                    uriBuilder = new UriBuilder(ipConfig.WebIP_Port);
                    uriBuilder.Path += apiURL.Trim();
                }
                else
                {
                    uriBuilder = new UriBuilder(apiURL.Trim());
                }
                uriBuilder.Path += urlGetVariables.Trim();

                return uriBuilder.Uri.ToString();
            }
        }
        public string token => accessToken.token.Trim();
        public string json => jsonString.json.Trim();
    }

    public enum RequestMethod
    {
        GET, HEAD, POST, PUT, CREATE, DELETE
    }
    public enum Authorization
    {
        Bearer
    }

    [Serializable]
    public struct AccessToken
    {
        [TextArea(1, 5)] public string token;
    }

    [Serializable]
    public struct JsonString
    {
        [TextArea(1, 100)] public string json;
    }

}