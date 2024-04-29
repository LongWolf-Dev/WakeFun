using UnityEngine;

[CreateAssetMenu(fileName = "�j���s", menuName = ">>WakeFun<</�j���s")]
public class MainButtonSO : ScriptableObject
{
    [Header(">>> ICON")]
    public Sprite icon;

    [Header(">>> ICON�C��")]
    public Color iconColor = new Color(1, 1, 1, 1);
    [Header(">>> �����C��")]
    public Color bkgColor = new Color(1, 1, 1, 1);

    [Header(">>> ��r")]
    public string btnLabel;
}
