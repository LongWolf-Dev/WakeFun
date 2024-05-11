using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace VictorDev.UI.Comps
{
    /// <summary>
    /// Toggle下拉式列表
    /// </summary>
    public class ToggleListButton : MonoBehaviour
    {
        [Header(">>> 資料集設定")]
        [SerializeField] private SO_ToggleListButton soToggleList;

        [Header(">>> 圖示")]
        [SerializeField] private Image icon;

        [Header(">>> Toggle按鈕標題")]
        [SerializeField] private TextMeshProUGUI title;
      

        [Header(">>> 下拉式清單容器")]
        [SerializeField] private Transform optionListContainer;

        [Header(">>> 下拉式清單項目")]
        [SerializeField] private ToggleList_Option optionPrefab;

        /// <summary>
        /// 當選項被點擊時，Invoke選項的文字
        /// </summary>
        public Action<string> onClickOptionEvent { get; set; }

        /// <summary>
        /// 設置SO_ToggleList資料集
        /// </summary>
        public SO_ToggleListButton SoToggleList { set => soToggleList = value;  }
        /// <summary>
        /// 設置按鈕標題
        /// </summary>
        /// <param name="label"></param>
        public void SetTitle(string label) =>title.SetText(label);
        /// <summary>
        /// 設置ToggleGroup
        /// </summary>
        public void SetToggleGroup(ToggleGroup tg) => GetComponent<Toggle>().group = tg;

        private void Start()
        {
            icon.sprite = soToggleList.Icon;
            title.SetText(soToggleList.Title);

            soToggleList.SoOptionList.ForEach(soOption =>
            {
                ToggleList_Option option = Instantiate(optionPrefab, optionListContainer);
                option.SetData(soOption);
                option.onClickEvent += OnOptionClicked;
            });

            if (soToggleList.SoOptionList.Count == 0)
            {
                GetComponent<Toggle>().onValueChanged.AddListener(
                    (isOn) =>
                    {
                        if (isOn) OnOptionClicked(soToggleList.Title);
                    });
            }
        }

        private void OnOptionClicked(string optionTitle) => onClickOptionEvent?.Invoke(optionTitle);

        private void OnValidate()
        {
            if (icon.sprite == soToggleList.Icon) return;
            icon.sprite = soToggleList.Icon;
            title.SetText(soToggleList.Title);
        }
    }
}
