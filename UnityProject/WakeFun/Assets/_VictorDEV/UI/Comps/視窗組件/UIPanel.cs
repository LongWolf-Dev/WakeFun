using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VictorDev.UI.Comps
{
    /// <summary>
    /// [abstract] 視窗組件 父類別
    /// </summary>
    public abstract class UIPanel : MonoBehaviour
    {
        [Header(">>> 標題文字")]
        [SerializeField] protected string titleText;

        public string TitleText
        {
            get => titleText;
            set
            {
                titleText = value;
                if (txtTitle != null) txtTitle.text = value;
            }
        }

        [Header(">>> 標題字樣")]
        [SerializeField] protected Text txtTitle;
        [Header(">>> 視窗關閉按鈕")]
        [SerializeField] protected Button btnClose;
        [Header(">>> 視窗最小化按鈕")]
        [SerializeField] protected Button btnMinimize;
        [Header(">>> 視窗縮放按鈕")]
        [SerializeField] protected Button btnScale;

        [Header(">>> 視窗內容器")]
        [SerializeField] protected Transform container;
        public Transform Container => container;

        [Header(">>> 當點擊關閉按鈕")]
        public UnityEvent<UIPanel> onClickCloseButton;
        [Header(">>> 當點擊最小化按鈕")]
        public UnityEvent<UIPanel> onClickMinimizeButton;
        [Header(">>> 當點擊縮放按鈕")]
        public UnityEvent<UIPanel, bool> onClickScaleButton;

        /// <summary>
        /// 是否為放大的狀態
        /// </summary>
        public bool isScaleOut { get; private set; } = false;

        private void Awake()
        {
            //監聽各項按鈕功能
            if (btnClose != null) btnClose.onClick.AddListener(() => onClickCloseButton?.Invoke(this));
            if (btnMinimize != null) btnMinimize?.onClick.AddListener(() => onClickMinimizeButton?.Invoke(this));
            if (btnScale != null) btnScale.onClick?.AddListener(() =>
            {
                isScaleOut = !isScaleOut;
                onClickScaleButton?.Invoke(this, isScaleOut);
                OnScaleStatusChanged();
            });
            OnAwake();
        }

        protected virtual void OnAwake() { }
        /// <summary>
        /// 當視窗Scale狀態改變時
        /// </summary>
        protected virtual void OnScaleStatusChanged() { }

        private void OnDestroy()
        {
            //移除各項按鈕功能
            if (btnClose != null) btnClose.onClick.RemoveAllListeners();
            if (btnMinimize != null) btnMinimize.onClick.RemoveAllListeners();
            if (btnScale != null) btnScale.onClick.RemoveAllListeners();
            OnDestroyAfter();
        }

        protected virtual void OnDestroyAfter() { }
    }
}
