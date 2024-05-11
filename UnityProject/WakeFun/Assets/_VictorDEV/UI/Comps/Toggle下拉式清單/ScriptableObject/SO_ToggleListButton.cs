using UnityEngine;

namespace VictorDev.UI.Comps
{
    [CreateAssetMenu(fileName = "Toggle下拉式清單 - 清單項目", menuName = ">>VictorDev<</UIComps/下拉式選單/Toggle下拉式清單 - 清單項目")]
    public class SO_ToggleList_Option : ScriptableObject
    {
        [Header("ICON圖示")]
        [SerializeField] private Sprite icon;
        [Header("按鈕顯示標題")]
        [SerializeField] private string title;

        public Sprite Icon => icon;
        public string Title => title;
    }
}
