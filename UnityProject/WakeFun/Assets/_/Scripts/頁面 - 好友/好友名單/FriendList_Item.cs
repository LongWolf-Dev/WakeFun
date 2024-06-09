using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 列表組件Item - 好友名單
/// </summary>
[RequireComponent(typeof(Button))]
public class FriendList_Item : MonoBehaviour
{
    [Header(">>> 列表組件Item - 好友名單")]
    [SerializeField] private SO_Account accountSO;

    [Header(">>> UI組件")]
    [SerializeField] private Button btnDetail;
    [SerializeField] private Toggle toggleLiked;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtNameTitle;

    /// <summary>
    /// 事件：點擊本體
    /// </summary>
    public UnityEvent<SO_Account> OnClickDetailButton;
    /// <summary>
    /// 事件：點擊+按鈕
    /// </summary>
    public UnityEvent<SO_Account, bool> OnClickLikedToggle;

    /// <summary>
    /// 設置Toggle讚的group
    /// </summary>
    public ToggleGroup toggleGroup { set => toggleLiked.group = value; }

    public SO_Account AccountSO
    {
        set
        {
            accountSO = value;
            RefreshData();
        }
    }

    private void Awake()
    {
        btnDetail.onClick.AddListener(() => OnClickDetailButton?.Invoke(accountSO));
        toggleLiked.onValueChanged.AddListener((isOn) => OnClickLikedToggle?.Invoke(accountSO, isOn));
    }

    private void OnValidate()
    {
        btnDetail ??= GetComponent<Button>();
        toggleLiked ??= transform.Find("Toggle讚").GetComponent<Toggle>();
        txtName ??= transform.Find("txt姓名").GetComponent<TextMeshProUGUI>();
        txtNameTitle ??= transform.Find("txt法號").GetComponent<TextMeshProUGUI>();
        RefreshData();
    }

    private void RefreshData()
    {
        if (accountSO != null)
        {
            if (txtName.text != accountSO.UserName)
            {
                txtName.text = accountSO.UserName;
                txtNameTitle.text = accountSO.TitleName;
            }
        }
        else
        {
            SO_Account tempData = ScriptableObject.CreateInstance<SO_Account>();
            tempData._SetupRandomData();
            txtName.text = tempData.UserName;
            txtNameTitle.text = tempData.TitleName;
        }
    }
}
