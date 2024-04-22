using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace VictorDev.Net
{
    public class WebImageLoader : MonoBehaviour
    {
        #region [ >>> protected禁止被別人實例化，並用instance單例模式 ]
        /// <summary>
        /// 已設定為protected，禁止被別人實例化
        /// </summary>

        protected static WebImageLoader instance;

        protected void Awake()
        {
            if (instance == null)
                instance = this;
        }

        protected void OnValidate() => name = "[static] WebImageLoader ";
        #endregion

        /// <summary>
        /// 儲存下載的圖片Texture(key值：url路徑)
        /// </summary>
        private static Dictionary<string, Texture> textureCacheList = new Dictionary<string, Texture>();

        /// <summary>
        /// 儲存所有的下載Task
        /// </summary>
        private static List<Task> taskList = new List<Task>();

        /// <summary>
        /// 當所有的下載Task完成時Invoke
        /// </summary>
        public static Action<Dictionary<string, Texture>> OnLoadAllWebImageCompleteEvent;

        /// <summary>
        /// 讀取多數網址圖片
        /// </summary>
        /// <param name="url">網址</param>
        /// <param name="OnSuccess">回傳Dictionary<url網址, Texture材質><param>
        /// <param name="OnFailed">回傳錯誤訊息</param>
        public static async void LoadAllWebImage(List<string> urlList, Action<Dictionary<string, Texture>> OnSuccess, Action<string> OnFailed)
        {
            foreach (string url in urlList)
            {
                LoadWebImage(url, null, OnFailed);
            }

            //等待所有非同步操作完成
            await Task.WhenAll(taskList);

            OnSuccess?.Invoke(textureCacheList);
            OnLoadAllWebImageCompleteEvent?.Invoke(textureCacheList);
        }

        /// <summary>
        /// 讀取單一網址圖片
        /// </summary>
        /// <param name="url">網址</param>
        /// <param name="OnSuccess">回傳Texture</param>
        /// <param name="OnFailed">回傳錯誤訊息</param>
        public static void LoadWebImage(string url, Action<Texture> OnSuccess, Action<string> OnFailed)
        {
            if (textureCacheList.ContainsKey(url) && textureCacheList[url] != null) OnSuccess?.Invoke(textureCacheList[url]);
            else
            {
                textureCacheList[url] = null; //先讓它有Key值
                instance.StartCoroutine(instance.LoadImageCoroutine(url, OnSuccess, OnFailed));
            }
        }

        private IEnumerator LoadImageCoroutine(string url, Action<Texture> OnSuccess, Action<string> OnFailed)
        {
            using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning($"Error loading image: {webRequest.error} / url: {url}");
                    OnFailed?.Invoke(webRequest.error);
                }
                else
                {
                    // 成功讀取圖片，將其設定為物件的主要材質
                    Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
                    textureCacheList[url] = texture;

                    OnSuccess?.Invoke(texture);
                }
            }
        }
    }
}
