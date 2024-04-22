using System.Collections.Generic;
using UnityEngine;

namespace VictorDev.Common
{
    /// <summary>
    /// LayerMask功能組件
    /// </summary>
    public abstract class LayerMaskHandler
    {
        /// <summary>
        /// 將目標物件的layer設置為指定LayerMask
        /// </summary>
        public static void SetGameObjectLayerToLayerMask(GameObject target, LayerMask layerMask)
        {
            int layer = 0;
            while (layerMask > 0)
            {
                layer++;
                layerMask = layerMask >> 1;
            }
            target.layer = layer - 1;

        }

        /// <summary>
        /// 擷取場景上所有LayerMask底下的物件
        /// </summary>
        public static List<Transform> GetGameObjectWithLayerMask(LayerMask layerMask)
        {
            List<Transform> result = new();
            GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in gameObjects)
            {
                if (IsSameLayerMask(obj, layerMask)) result.Add(obj.transform);
            }
            return result;
        }

        /// <summary>
        /// 擷取場景裡指定LayerMask底下具有指定Component的物件
        /// </summary>
        /// <returns>List [T]：指定Component</returns>
        public static List<T> GetGameObjectWithLayerMask<T>(LayerMask layerMask)
        {
            List<T> resultObjectList = new();
            List<Transform> layerObjects = GetGameObjectWithLayerMask(layerMask);
            foreach (Transform obj in layerObjects)
            {
                if (obj.TryGetComponent<T>(out T component)) resultObjectList.Add(obj.GetComponent<T>());
            }
            return resultObjectList;
        }

        /// <summary>
        /// 新增指定的Layer到目標攝影機的CullingMask
        /// </summary>
        public static void AddLayerMaskToCamera(Camera targetCamera, LayerMask layerMask) => targetCamera.cullingMask |= layerMask;

        /// <summary>
        /// 從目標攝影機的CullingMask裡移除指定的Layer
        /// </summary>
        public static void RemoveLayerMaskFromCamera(Camera targetCamera, LayerMask layerMask) => targetCamera.cullingMask &= ~layerMask.value;

        /// <summary>
        /// 檢查目標GameObject是否為指定的LayerMask
        /// </summary>
        public static bool IsSameLayerMask(GameObject targetObject, LayerMask layerMask) => (layerMask.value & (1 << targetObject.layer)) != 0;
    }
}
