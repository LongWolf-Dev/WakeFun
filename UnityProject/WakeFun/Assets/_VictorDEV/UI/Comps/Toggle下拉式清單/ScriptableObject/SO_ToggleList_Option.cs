using DG.Tweening.Core;
using System.Collections.Generic;
using UnityEngine;

namespace VictorDev.UI.Comps
{
    [CreateAssetMenu(fileName = "Toggle下拉式清單", menuName = ">>VictorDev<</UIComps/下拉式選單/Toggle下拉式清單")]
    public class SO_ToggleListButton : ScriptableObject
    {
        [Header("ICON圖示")]
        [SerializeField] private Sprite icon;
        [Header("Toggle按鈕顯示標題")]
        [SerializeField] private string title;
        [Header("Toggle點擊後顯示的項目清單")]
        [SerializeField] private List<SO_ToggleList_Option> soOptionList;

        public Sprite Icon => icon;
        public string Title => title;
        public List<SO_ToggleList_Option> SoOptionList => soOptionList;
    }
}
