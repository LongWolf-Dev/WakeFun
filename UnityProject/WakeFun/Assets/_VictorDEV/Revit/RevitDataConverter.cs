using System.Collections.Generic;
using UnityEngine;
using VictorDev.Parser;

namespace VictorDev.RevitUtils
{
    /// <summary>
    ///  Revit資料轉換
    /// </summary>
    public static class RevitDataConverter
    {
        /// <summary>
        /// Rotation轉換 (-1：往左 / 1：往右 / 0.5：往上 /  0.5：往下)
        /// </summary>
        public static Quaternion ConvertToRotation(string columnValue)
        {
            float rotationY = 0;
            switch (columnValue)
            {
                case "-1": rotationY = 270; break;
                case "1": rotationY = 90; break;
                case "0.5": rotationY = 0; break;
                case "-0.5": rotationY = 180; break;
            }
            return Quaternion.Euler(new Vector3(0, rotationY, 0));
        }

        /// <summary>
        /// 取得指定Name的String值
        /// </summary>
        public static string GetStringValue(string jsonData, string valueName) => GetStringValue(JsonUtils.ParseJson(jsonData), valueName);
        /// <summary>
        /// 取得指定Name的String值
        /// </summary>
        public static string GetStringValue(Dictionary<string, string> dictData, string valueName) => dictData[valueName];

        /// <summary>
        /// 取得指定Name的Float值
        /// </summary>
        public static float GetFloatValue(string jsonData, string valueName) => GetFloatValue(JsonUtils.ParseJson(jsonData), valueName);
        /// <summary>
        /// 取得指定Name的Float值
        /// </summary>
        public static float GetFloatValue(Dictionary<string, string> dictData, string valueName) => float.Parse(dictData[valueName]);

        /// <summary>
        /// 座標轉換 (Revit的Z軸 = Unity的Y軸)
        /// </summary>
        public static Vector3 ConvertToPosition(string jsonData) => ConvertToPosition(JsonUtils.ParseJson(jsonData));
        /// <summary>
        /// 座標轉換 (Revit的Z軸 = Unity的Y軸)
        /// </summary>
        public static Vector3 ConvertToPosition(Dictionary<string, string> position) => new Vector3(GetFloatValue(position, "x"), GetFloatValue(position, "z"), GetFloatValue(position, "y"));
        //public static Vector3 ConvertToPosition(Dictionary<string, string> position) => new Vector3(float.Parse(position["x"]), float.Parse(position["z"]), float.Parse(position["y"]));
        /// <summary>
        /// 座標轉換 (Revit的Z軸 = Unity的Y軸)
        /// </summary>
        public static Vector3 ConvertToPosition(Vector3 position) => new Vector3(position.x, position.z, position.y);


        /// <summary>
        /// Scale轉換 
        /// </summary>
        public static Vector3 ConvertToScale(string jsonData) => ConvertToScale(JsonUtils.ParseJson(jsonData));

        /// <summary>
        /// Scale轉換 
        /// </summary>
        public static Vector3 ConvertToScale(Dictionary<string, string> position) => new Vector3(float.Parse(position["i"]), float.Parse(position["h"]), float.Parse(position["w"]));
    }
}

