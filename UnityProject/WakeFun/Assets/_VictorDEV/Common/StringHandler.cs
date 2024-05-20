using Newtonsoft.Json;
using System;
using System.Text;

namespace VictorDev.Common
{
    public abstract class StringHandler
    {
        /// <summary>
        /// 設置文字大小(HTML)
        /// </summary>
        public static string SetFontSizeString(string str, int fontSize) => $"<size='{fontSize}'>{str}</size>";

        /// <summary>
        /// 解碼Base64 byte[] 轉成UTF8字串
        /// </summary>
        public static string Base64ToString(byte[] data)
        {
            string base64String = JsonConvert.SerializeObject(data).Trim('\"');
            byte[] byteArray = Convert.FromBase64String(base64String);
            // 將 byte[] 解碼為字符串
            return Encoding.UTF8.GetString(byteArray);
        }
    }
}
