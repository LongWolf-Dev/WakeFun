using System.Collections.Generic;
using UnityEngine;

namespace VictorDev.Common
{
    public abstract class GameObjectHandler
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
                if (child.TryGetComponent<T>(out T component))  result.Add(component);
                if (child.childCount > 0)   result.AddRange(GetAllChildOfTargetWithComponent<T>(child));
            }
            return result;
        }
    }
}
