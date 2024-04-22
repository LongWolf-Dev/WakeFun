using System.Collections.Generic;
using UnityEngine;

namespace VictorDev.Common
{
    /// <summary>
    /// LayerMask�\��ե�
    /// </summary>
    public abstract class LayerMaskHandler
    {
        /// <summary>
        /// �N�ؼЪ���layer�]�m�����wLayerMask
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
        /// �^�������W�Ҧ�LayerMask���U������
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
        /// �^�������̫��wLayerMask���U�㦳���wComponent������
        /// </summary>
        /// <returns>List [T]�G���wComponent</returns>
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
        /// �s�W���w��Layer��ؼ���v����CullingMask
        /// </summary>
        public static void AddLayerMaskToCamera(Camera targetCamera, LayerMask layerMask) => targetCamera.cullingMask |= layerMask;

        /// <summary>
        /// �q�ؼ���v����CullingMask�̲������w��Layer
        /// </summary>
        public static void RemoveLayerMaskFromCamera(Camera targetCamera, LayerMask layerMask) => targetCamera.cullingMask &= ~layerMask.value;

        /// <summary>
        /// �ˬd�ؼ�GameObject�O�_�����w��LayerMask
        /// </summary>
        public static bool IsSameLayerMask(GameObject targetObject, LayerMask layerMask) => (layerMask.value & (1 << targetObject.layer)) != 0;
    }
}
