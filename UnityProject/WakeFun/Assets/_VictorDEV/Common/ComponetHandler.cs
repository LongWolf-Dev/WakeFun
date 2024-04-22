using System;
using System.Collections.Generic;
using UnityEngine;
using Debug = VictorDev.Common.DebugHandler;

namespace VictorDev.ComponentUtils
{
    public abstract class ComponetHandler
    {
        /// <summary>
        /// 擷取目標物件底下一層，具有指定Component的GameObject
        /// </summary>
        public static List<T> GetChildOfTargetWithComponent<T>(Transform target)
        {
            List<T> result = new();
            foreach (Transform child in target)
            {
                if (child.TryGetComponent<T>(out T component)) result.Add(component);
            }
            return result;
        }

        /// <summary>
        /// 擷取目標物件底下所有具有指定Component的GameObject
        /// </summary>
        public static List<T> GetAllChildOfTargetWithComponent<T>(Transform target)
        {
            List<T> result = new();
            foreach (Transform child in target)
            {
                if (child.TryGetComponent<T>(out T component)) result.Add(component);
                if (child.childCount > 0) result.AddRange(GetAllChildOfTargetWithComponent<T>(child));
            }
            return result;
        }


        /// <summary>
        /// 新增Component到目標物件上 (comps參數要加上typeof)
        /// </summary>
        public static Component AddComponentToTarget(Transform target, Type comps) => AddComponentToTarget(target.gameObject, comps);
        public static Component AddComponentToTarget(GameObject target, Type comps)
        {
            if (target.TryGetComponent(comps, out Component existComps))
            {
                Debug.Log($">>>  [{target.name}] 物件已擁有 [{comps.Name}]");
                return existComps;
            }
            else
            {
                Debug.Log($"\t + 新增 [{comps.Name}] 至 [{target.name}]");
                return target.gameObject.AddComponent(comps);
            }
        }

        /// <summary>
        /// 從目標物件上移除Component (comps參數要加上typeof)
        /// </summary>
        public static void RemoveComponentFromTarget(Transform target, Type comps) => RemoveComponentFromTarget(target.gameObject, comps);
        public static void RemoveComponentFromTarget(GameObject target, Type comps)
        {
            if (target.TryGetComponent(comps, out Component existComps))
            {
                Debug.Log($"\t - 從 [{target.name}] 移除 [{existComps.name}]");
                GameObject.DestroyImmediate(existComps);
            }
        }
    }
}
