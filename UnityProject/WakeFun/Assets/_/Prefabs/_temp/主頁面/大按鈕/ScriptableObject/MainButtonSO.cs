using UnityEngine;

[CreateAssetMenu(fileName = "大按鈕", menuName = ">>WakeFun<</大按鈕")]
public class MainButtonSO : ScriptableObject
{
    [Header(">>> ICON")]
    public Sprite icon;

    [Header(">>> ICON顏色")]
    public Color iconColor = new Color(1, 1, 1, 1);
    [Header(">>> 底部顏色")]
    public Color bkgColor = new Color(1, 1, 1, 1);

    [Header(">>> 文字")]
    public string btnLabel;
}
