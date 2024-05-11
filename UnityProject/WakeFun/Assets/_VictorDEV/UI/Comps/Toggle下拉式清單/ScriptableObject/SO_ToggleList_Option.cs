using DG.Tweening.Core;
using System.Collections.Generic;
using UnityEngine;

namespace VictorDev.UI.Comps
{
    [CreateAssetMenu(fileName = "Toggle�U�Ԧ��M��", menuName = ">>VictorDev<</UIComps/�U�Ԧ����/Toggle�U�Ԧ��M��")]
    public class SO_ToggleListButton : ScriptableObject
    {
        [Header("ICON�ϥ�")]
        [SerializeField] private Sprite icon;
        [Header("Toggle���s��ܼ��D")]
        [SerializeField] private string title;
        [Header("Toggle�I������ܪ����زM��")]
        [SerializeField] private List<SO_ToggleList_Option> soOptionList;

        public Sprite Icon => icon;
        public string Title => title;
        public List<SO_ToggleList_Option> SoOptionList => soOptionList;
    }
}
