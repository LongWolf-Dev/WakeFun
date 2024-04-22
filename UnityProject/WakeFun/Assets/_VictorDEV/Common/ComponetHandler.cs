using System;
using System.Collections.Generic;
using UnityEngine;
using Debug = VictorDev.Common.DebugHandler;

namespace VictorDev.ComponentUtils
{
    public abstract class ComponetHandler
    {
        /// <summary>
        /// �^���ؼЪ��󩳤U�@�h�A�㦳���wComponent��GameObject
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
        /// �^���ؼЪ��󩳤U�Ҧ��㦳���wComponent��GameObject
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
        /// �s�WComponent��ؼЪ���W (comps�Ѽƭn�[�Wtypeof)
        /// </summary>
        public static Component AddComponentToTarget(Transform target, Type comps) => AddComponentToTarget(target.gameObject, comps);
        public static Component AddComponentToTarget(GameObject target, Type comps)
        {
            if (target.TryGetComponent(comps, out Component existComps))
            {
                Debug.Log($">>>  [{target.name}] ����w�֦� [{comps.Name}]");
                return existComps;
            }
            else
            {
                Debug.Log($"\t + �s�W [{comps.Name}] �� [{target.name}]");
                return target.gameObject.AddComponent(comps);
            }
        }

        /// <summary>
        /// �q�ؼЪ���W����Component (comps�Ѽƭn�[�Wtypeof)
        /// </summary>
        public static void RemoveComponentFromTarget(Transform target, Type comps) => RemoveComponentFromTarget(target.gameObject, comps);
        public static void RemoveComponentFromTarget(GameObject target, Type comps)
        {
            if (target.TryGetComponent(comps, out Component existComps))
            {
                Debug.Log($"\t - �q [{target.name}] ���� [{existComps.name}]");
                GameObject.DestroyImmediate(existComps);
            }
        }
    }
}
