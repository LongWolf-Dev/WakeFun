using System.Collections.Generic;
using UnityEngine;

namespace VictorDev.Common
{
    /// <summary>
    /// 各組件之初始化先後順序管理
    /// <para> + 在Awake時執行Initialize()初始化</para>
    /// </summary>
    public class LifecycleManager : SingletonMonoBehaviour<LifecycleManager>
    {
        [Header(">>> LifecycleComponent組件排序先後初始化")]
        [SerializeField] private List<LifecycleComponent> lifecycleComps;

        /// <summary>
        /// Awake時進行初始化
        /// </summary>
        private void Awake()
        {
            foreach (LifecycleComponent item in lifecycleComps)
            {
                item.Initialize();
            }
        }
    }

    /// <summary>
    /// 由於Interface無法顯示於Inspector，所以用抽象類別當作Interface
    /// <para>+ 組件繼承LifecycleWrapper即可</para>
    /// </summary>
    public abstract class LifecycleComponent : MonoBehaviour
    {
        /// <summary>
        /// 組件初始化
        /// </summary>
        public virtual void Initialize()
        {
        }
    }
}
