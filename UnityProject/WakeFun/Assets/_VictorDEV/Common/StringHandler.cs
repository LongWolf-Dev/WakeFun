namespace VictorDev.Common
{
    public abstract class StringHandler
    {
        /// <summary>
        /// 設置文字大小(HTML)
        /// </summary>
        public static string SetFontSizeString(string str, int fontSize) => $"<size='{fontSize}'>{str}</size>";
    }
}
