using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VictorDev.UI.Comps
{
    /// <summary>
    /// Toggle下拉式列表的清單選項
    /// </summary>
    public class ToggleList_Option : MonoBehaviour
    {
        [SerializeField] private SO_ToggleList_Option data;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI txt;
        [SerializeField] private Button btn;

        public Action<string> onClickEvent;

        private void Awake()
        {
            btn.onClick.AddListener(() => onClickEvent?.Invoke(data.Title));
        }

        public void SetData(SO_ToggleList_Option data)
        {
            this.data = data;
            icon.gameObject.SetActive(data.Icon != null);
            if (icon.gameObject.activeSelf) icon.sprite = data.Icon;
            txt.text = data.Title;
        }
    }
}
