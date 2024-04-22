using UnityEngine;
using UnityEngine.UI;

public class Circle_MainButton : MonoBehaviour
{
    [SerializeField] private Circle_MainButtonSO circleMainButtonSO;
    [SerializeField] private Image image;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Text btnLabel;

    [ContextMenu("SO設定")]
    private void Awake()
    {
        image.sprite = circleMainButtonSO.spriteNormal;

        // 获取Button的SpriteState
        SpriteState spriteState = toggle.spriteState;
        // 设置Pressed Sprite
        spriteState.pressedSprite = circleMainButtonSO.spriteNormal;
        // 应用修改
        toggle.spriteState = spriteState;

        btnLabel.text = circleMainButtonSO.btnLabel;
    }

    private void OnValidate() => name = $"圓形大按鈕 - {circleMainButtonSO.btnLabel}";
}