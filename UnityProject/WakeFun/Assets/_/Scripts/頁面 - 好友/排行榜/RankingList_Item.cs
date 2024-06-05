using TMPro;
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
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtNameTitle;

    /// <summary>
    /// 事件：點擊本體
    /// </summary>
    public UnityEvent<SO_Account> OnClickDetailButton;
    /// <summary>
    /// 事件：點擊新增好友按鈕
    /// </summary>
    public UnityEvent<SO_Account> OnClickAddFriendButton;

    public SO_Account achievementSO
    {
        set
        {
            accountSO = value;
            RefreshData();
        }
    }

    private void Awake()
    {
        btnDetail.onClick.AddListener(() => OnClickDetailButton.Invoke(accountSO));
        btnAddFriend.onClick.AddListener(() => OnClickAddFriendButton?.Invoke(accountSO));
    }
    private void OnValidate()
    {
        btnDetail ??= GetComponent<Button>();
        btnAddFriend ??= transform.Find("新增好友").GetComponent<Button>();
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

            accountSO = tempData;
        }
    }
}
