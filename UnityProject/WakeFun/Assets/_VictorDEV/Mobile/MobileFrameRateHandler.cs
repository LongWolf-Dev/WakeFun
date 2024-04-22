using System.Collections;
using System.Threading;
using UnityEngine;

namespace VictorDev.Mobile
{
    /// <summary>
    /// 設定App最大FPS數限制 (系統預設30)
    /// </summary>
    public class MobileFrameRateHandler : MonoBehaviour
    {
        [Range(30, 120)]
        [SerializeField] private float TargetFrameRate = 60.0f;

        private int maxRate = 9999;
        private float currentFrameTime = 0;

        private void Awake()
        {
            if (Input.touchPressureSupported)
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = maxRate;
                currentFrameTime = Time.realtimeSinceStartup;
                StartCoroutine(WaitForNextFrame());
            }
        }

        private IEnumerator WaitForNextFrame()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                currentFrameTime += 1.0f / TargetFrameRate;
                var t = Time.realtimeSinceStartup;
                var sleepTime = currentFrameTime - t - 0.01f;
                if (sleepTime > 0)
                {
                    Thread.Sleep((int)(sleepTime * 1000));
                }
                while (t < currentFrameTime)
                {
                    t = Time.realtimeSinceStartup;
                }
            }
        }
    }
}
