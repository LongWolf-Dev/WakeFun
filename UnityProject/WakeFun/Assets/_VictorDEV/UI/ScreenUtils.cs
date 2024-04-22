using Tayx.Graphy.Utils.NumString;
using UnityEngine;

namespace VictorDev.UI
{
    public abstract class ScreenUtils
    {
        #region [>>> Private Variables]
        private static Vector2 resolution => new Vector2(Screen.width, Screen.height);
        private static Vector2 window => new Vector2(int.Parse(Screen.width.ToStringNonAlloc()), int.Parse(Screen.height.ToStringNonAlloc()));
        #endregion

        /// <summary>
        /// �ù��ѪR��
        /// </summary>
        public static Vector2 screenResolution => resolution;

        /// <summary>
        /// Unity�e���ئT
        /// </summary>
        public static Vector2 windowSize => window;
    }
}
