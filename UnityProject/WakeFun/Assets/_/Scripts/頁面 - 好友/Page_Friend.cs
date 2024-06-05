using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 頁面 - 好友
/// </summary>
public class Page_Friend : MonoBehaviour
{
    [Header(">>> 返回鈕")]
    [SerializeField] private Button btnReturn;

    [Header(">>> 排行榜列表")]
    [SerializeField] private RankingList rankingList;
    [Header(">>> 好友名單列表")]
    [SerializeField] private FriendList friendList; 
    [Header(">>> 新增好友頁面")]
    [SerializeField] private AddFriendHandler addFriendHandler;
    [Header(">>> 帳號詳細資訊")]
    [SerializeField] private AccountDetailPage accountDetailPage;

    private void Awake()
    {
        rankingList.OnClickDetail.AddListener(accountDetailPage.SetAccountSoData);
        friendList.OnClickDetail.AddListener(accountDetailPage.SetAccountSoData);
        addFriendHandler.OnClickAvatarEvent.AddListener(accountDetailPage.SetAccountSoData);
    }

    private void OnValidate()
    {
        btnReturn ??= transform.GetChild(0).Find("Button返回").GetComponent<Button>();
        rankingList ??= transform.Find("排行榜列表").GetComponent<RankingList>();
        friendList ??= transform.Find("好友名單列表").GetComponent<FriendList>();
        addFriendHandler ??= transform.Find("新增好友頁面").GetComponent<AddFriendHandler>();
        accountDetailPage ??= transform.Find("帳號詳細資訊").GetComponent<AccountDetailPage>();
    }
}
