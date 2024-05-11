using UnityEngine;

namespace VictorDev.UI.Comps
{
    [CreateAssetMenu(fileName = "Toggle�U�Ԧ��M�� - �M�涵��", menuName = ">>VictorDev<</UIComps/�U�Ԧ����/Toggle�U�Ԧ��M�� - �M�涵��")]
    public class SO_ToggleList_Option : ScriptableObject
    {
        [Header("ICON�ϥ�")]
        [SerializeField] private Sprite icon;
        [Header("���s��ܼ��D")]
        [SerializeField] private string title;

        public Sprite Icon => icon;
        public string Title => title;
    }
}
