using UnityEngine;

[CreateAssetMenu(fileName = "圓形大按鈕", menuName = ">>WakeFun<</圓形大按鈕")]
public class Circle_MainButtonSO : ScriptableObject
{
    [Header(">>> 圖片 - 平常時")]
    public Sprite spriteNormal;
    [Header(">>> 圖片 - 選取時")]
    public Sprite spriteSelected;

    [Header(">>> 文字")]
    public string btnLabel;
}
