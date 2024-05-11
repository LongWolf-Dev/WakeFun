using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace VictorDev.UI.Comps
{
    /// <summary>
    /// Toggle�U�Ԧ��C��
    /// </summary>
    public class ToggleListButton : MonoBehaviour
    {
        [Header(">>> ��ƶ��]�w")]
        [SerializeField] private SO_ToggleListButton soToggleList;

        [Header(">>> �ϥ�")]
        [SerializeField] private Image icon;

        [Header(">>> Toggle���s���D")]
        [SerializeField] private TextMeshProUGUI title;
      

        [Header(">>> �U�Ԧ��M��e��")]
        [SerializeField] private Transform optionListContainer;

        [Header(">>> �U�Ԧ��M�涵��")]
        [SerializeField] private ToggleList_Option optionPrefab;

        /// <summary>
        /// ��ﶵ�Q�I���ɡAInvoke�ﶵ����r
        /// </summary>
        public Action<string> onClickOptionEvent { get; set; }

        /// <summary>
        /// �]�mSO_ToggleList��ƶ�
        /// </summary>
        public SO_ToggleListButton SoToggleList { set => soToggleList = value;  }
        /// <summary>
        /// �]�m���s���D
        /// </summary>
        /// <param name="label"></param>
        public void SetTitle(string label) =>title.SetText(label);
        /// <summary>
        /// �]�mToggleGroup
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
