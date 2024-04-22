using UnityEngine;
using UnityEngine.UI;

public class MainButton : MonoBehaviour
{
    [SerializeField] private MainButtonSO mainButtonSO;
    [SerializeField] private Image icon, bkgImage;
    [SerializeField] private Text btnLabel;

    [ContextMenu("SO³]©w")]
    private void Awake()
    {
        icon.sprite = mainButtonSO.icon;
        icon.color = mainButtonSO.iconColor;
        bkgImage.color = mainButtonSO.bkgColor;
        btnLabel.text = mainButtonSO.btnLabel;
    }

    private void OnValidate() => name = $"¤j«ö¶s - {mainButtonSO.btnLabel}";
}