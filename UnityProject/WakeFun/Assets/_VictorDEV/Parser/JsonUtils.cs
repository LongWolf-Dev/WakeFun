using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace VictorDev.Parser
{
    /// <summary>
    /// JSON資料解析 (使用static)
    /// <para>+ 不可被實例化</para>
    /// </summary>
    public abstract class JsonUtils
    {
        /// <summary>
        /// 解析Json字串資料
        /// <para>使用方式：JObject.Parse(jsonString)</para>
        /// </summary>
        /// <param name="jsonData">Json字串資料</param>
        /// <returns>單一Json物件：Dictionary物件[欄位名, 值]</returns>
        public static Dictionary<string, string> ParseJson(string jsonData)
            => SetupJsonDictionaryItem(JObject.Parse(jsonData));

        /// <summary>
        /// 解析Json陣列資料 [ Json字串 ]
        /// <para>使用方式：JArray.Parse(jsonString)</para>
        /// </summary>
        /// <param name="jsonData">Json字串資料</param>
        /// <returns>Json物件陣列 - List[Dictionary[[欄位名, 值]]</returns>
        public static List<Dictionary<string, string>> ParseJsonArray(string jsonData)
        {
            List<Dictionary<string, string>> resultDictList = new List<Dictionary<string, string>>();
            JArray jsonArray = JArray.Parse(jsonData);
            foreach (JObject jsonObject in jsonArray)
            {
                resultDictList.Add(SetupJsonDictionaryItem(jsonObject));
            }
            return resultDictList;
        }

        #region [>>> Private Functions]
        /// <summary>
        /// 設置Json Dictionary物件
        /// </summary>
        /// <param name="jsonObject">JObject.Parse後的資料</param>
        /// <returns>單一Json物件 Dictionary[string, string]</returns>
        private static Dictionary<string, string> SetupJsonDictionaryItem(JObject jsonObject)
        {
            Dictionary<string, string> resultJsonItem = new Dictionary<string, string>();
            foreach (JProperty property in jsonObject.Properties())
            {
                resultJsonItem[property.Name] = jsonObject[property.Name].ToString();
            }
            return resultJsonItem;
        }
        #endregion
    }
}
