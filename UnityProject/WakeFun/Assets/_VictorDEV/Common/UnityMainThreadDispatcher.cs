using System;
using System.Collections.Generic;
using UnityEngine;

namespace VictorDev.Common
{
    /// <summary>
    /// 主线程调度器，确保在主线程上执行更新操作
    /// <para>+ 需要依靠MonoBehaviour.Update()來對主線程上的更新</para>
    /// <para>+ 自動偵測實例是否為null，以新增實例</para>
    /// </summary>
    public class UnityMainThreadDispatcher : MonoBehaviour
    {
        private static UnityMainThreadDispatcher instance;

        /// <summary>
        /// 儲存欲執行的Action
        /// </summary>
        private readonly Queue<Action> actionQueue = new Queue<Action>();

        private static UnityMainThreadDispatcher Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<UnityMainThreadDispatcher>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject(">>Dynamic<< UnityMainThreadDispatcher");
                        instance = obj.AddComponent<UnityMainThreadDispatcher>();
                    }
                }
                return instance;
            }
        }

        private void Awake() => instance ??= this;

        private void Update()
        {
            while (actionQueue.Count > 0)
            {
                actionQueue.Dequeue().Invoke();
            }
        }

        /// <summary>
        /// 新增欲進行的Action
        /// </summary>
        public static void Enqueue(Action action)
        {
            if (action == null)
            {
                Debug.LogWarning("Enqueueing a null action, ignoring.");
                return;
            }
            Instance.actionQueue.Enqueue(action);
        }

        private void OnValidate()
        {
            name = ">> Dynamic << UnityMainThreadDispatcher";
        }
    }
}