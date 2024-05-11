using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 列表組件Item - 排行榜
/// </summary>
[RequireComponent(typeof(Button))]
public class RankingList_Item : MonoBehaviour
{
    [Header(">>> 列表組件Item - 排行榜")]
    [SerializeField] private SO_Account accountSO;

    [SerializeField] private Button btnDetail;
    [SerializeField] private Button btnAddFriend;
    [SerializeField] private Text txtName;
    [SerializeField] private Text txtNameTitle;

    /// <summary>
    /// 事件：點擊本體
    /// </summary>
    public UnityEvent<SO_Account> OnClickDetailButton;
    /// <summary>
    /// 事件：點擊+按鈕
    /// </summary>
    public UnityEvent<SO_Account> OnClickAddFriendButton;

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
        btnAddFriend.onClick.AddListener(() => OnClickAddFriendButton?.Invoke(accountSO));
    }

    private void OnValidate()
    {
        btnDetail ??= GetComponent<Button>();
        btnAddFriend ??= transform.Find("新增好友").GetComponent<Button>();
        txtName ??= transform.Find("姓名").GetComponent<Text>();
        txtNameTitle ??= transform.Find("法號").GetComponent<Text>();
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
                name = $"{GetType().Name} - {accountSO.UserName}";
            }
        }
        else
        {
            SO_Account tempData = ScriptableObject.CreateInstance<SO_Account>();
            tempData._SetupRandomData();
            txtName.text = tempData.UserName;
            txtNameTitle.text = tempData.TitleName;
            name = $"{GetType().Name} - {tempData.UserName}";

            Debug.LogWarning($"[Random Data] >>> {name}");
        }
    }
}
