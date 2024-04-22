using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;
using VictorDev.Parser;

namespace VictorDev.Net.WebAPI
{
    /// <summary>
    /// WebAPI呼叫 (以Coroutine方式呼叫)
    /// </summary>
    public abstract class WebAPI_Handler
    {
        /// <summary>
        /// 呼叫WebAPI(用URL，預設RequestPacakge) (單一JSON資料)
        /// </summary>
        /// <param name="url">網址</param>
        /// <param name="onSuccess">成功，回傳Dictionary<欄位名，值></param>
        /// <param name="onFailed">失敗，回傳錯誤訊息</param>
        public static IEnumerator CallWebAPI(string url, Action<long, Dictionary<string, string>> onSuccess, Action<long, string> onFailed = null)
             => CallWebAPI(new WebAPI_RequestPackage(url), onSuccess, onFailed);

        /// <summary>
        /// 呼叫WebAPI(用RequestPackage) (單一JSON資料)
        /// </summary>
        /// <param name="url">網址</param>
        /// <param name="onSuccess">成功，回傳Dictionary<欄位名，值></param>
        /// <param name="onFailed">失敗，回傳錯誤訊息</param>
        public static IEnumerator CallWebAPI(WebAPI_RequestPackage requestPackage, Action<long, Dictionary<string, string>> onSuccess, Action<long, string> onFailed = null)
             => CoroutineHandler.RunCoroutine(SendRequestCoroutine(requestPackage, onSuccess, onFailed));

        /// <summary>
        /// 呼叫WebAPI(用URL，預設RequestPacakge) (陣列JSON資料)
        /// </summary>
        /// <param name="url">網址</param>
        /// <param name="onSuccess">成功，回傳Dictionary<欄位名，值></param>
        /// <param name="onFailed">失敗，回傳錯誤訊息</param>
        public static IEnumerator CallWebAPI(string url, Action<long, List<Dictionary<string, string>>> onSuccess, Action<long, string> onFailed = null)
             => CallWebAPI(new WebAPI_RequestPackage(url), onSuccess, onFailed);

        /// <summary>
        /// 呼叫WebAPI(用RequestPackage) (陣列JSON資料)
        /// </summary>
        /// <param name="url">網址</param>
        /// <param name="onSuccess">成功，回傳Dictionary<欄位名，值></param>
        /// <param name="onFailed">失敗，回傳錯誤訊息</param>
        public static IEnumerator CallWebAPI(WebAPI_RequestPackage requestPackage, Action<long, List<Dictionary<string, string>>> onSuccess, Action<long, string> onFailed = null)
             => CoroutineHandler.RunCoroutine(SendRequestCoroutine(requestPackage, onSuccess, onFailed));


        /// <summary>
        /// 發送請求 (回傳：單一JSON值)
        /// </summary>
        private static IEnumerator SendRequestCoroutine(WebAPI_RequestPackage requestPackage, Action<long, Dictionary<string, string>> onSuccess, Action<long, string> onFailed)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(requestPackage.url))
            {
                // 設定Request相關資訊
                DownloadHandler downloadHandler = RequestSetting(requestPackage, request);
                // 發送請求
                yield return request.SendWebRequest();
                // 處理結果資訊
                ResultHandler(onSuccess, onFailed, request, downloadHandler);
            }
        }
        /// <summary>
        /// 發送請求 (回傳：JSON值陣列)
        /// </summary>
        private static IEnumerator SendRequestCoroutine(WebAPI_RequestPackage requestPackage, Action<long, List<Dictionary<string, string>>> onSuccess, Action<long, string> onFailed)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(requestPackage.url))
            {
                // 設定Request相關資訊
                DownloadHandler downloadHandler = RequestSetting(requestPackage, request);
                // 發送請求
                yield return request.SendWebRequest();
                // 處理結果資訊
                ResultHandler(onSuccess, onFailed, request, downloadHandler);
            }
        }

        /// <summary>
        /// 設定Request相關資訊
        /// </summary>
        private static DownloadHandler RequestSetting(WebAPI_RequestPackage requestPackage, UnityWebRequest request)
        {
            // 設定Header資訊
            request.SetRequestHeader("Content-Type", "application/json");
            if (string.IsNullOrEmpty(requestPackage.token) == false)
            {
                request.SetRequestHeader("Authorization", $"{requestPackage.authorization} {requestPackage.token}");
            }

            DownloadHandler downloadHandler;
            if (requestPackage.method == RequestMethod.POST || requestPackage.method == RequestMethod.PUT)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(requestPackage.json);
                UploadHandler uploadHandler = new UploadHandlerRaw(bytes)
                {
                    contentType = "application/json"
                };
                request.uploadHandler = uploadHandler;
            }
            downloadHandler = new DownloadHandlerBuffer();
            request.downloadHandler = downloadHandler;
            return downloadHandler;
        }
        /// <summary>
        /// 處理結果資訊 (回傳：單一JSON值)
        /// </summary>
        private static void ResultHandler(Action<long, Dictionary<string, string>> onSuccess, Action<long, string> onFailed, UnityWebRequest request, DownloadHandler downloadHandler)
        {
            if (request.result == UnityWebRequest.Result.ConnectionError
               || request.result == UnityWebRequest.Result.ProtocolError)
            {
                //失敗
                onFailed?.Invoke(request.responseCode, request.error);
            }
            else
            {
                //成功，回傳Dictionary<欄位名, 值>
                onSuccess?.Invoke(request.responseCode, JsonUtils.ParseJson(downloadHandler.text));
            }
        }
        /// <summary>
        /// 處理結果資訊 (回傳：JSON值陣列)
        /// </summary>
        private static void ResultHandler(Action<long, List<Dictionary<string, string>>> onSuccess, Action<long, string> onFailed, UnityWebRequest request, DownloadHandler downloadHandler)
        {
            if (request.result == UnityWebRequest.Result.ConnectionError
               || request.result == UnityWebRequest.Result.ProtocolError)
            {
                //失敗
                onFailed?.Invoke(request.responseCode, request.error);
            }
            else
            {
                //成功，回傳Dictionary<欄位名, 值>
                onSuccess?.Invoke(request.responseCode, JsonUtils.ParseJsonArray(downloadHandler.text));
            }
        }
    }
}
