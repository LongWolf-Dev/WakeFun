using UnityEngine;
using UnityEngine.UI;

public class PageHandler_Friend : MonoBehaviour
{
    [Header(">>> ��^�s")]
    [SerializeField] private Button btnReturn;

    [Header(">>> �Ʀ�]�C��")]
    [SerializeField] private RankingList rankingList;
    [Header(">>> �n�ͦW��C��")]
    [SerializeField] private FriendList friendList;
    [Header(">>> �s�W�n�ͭ���")]
    [SerializeField] private AddFriendHandler addFriendHandler;
    [Header(">>> �b���ԲӸ�T")]
    [SerializeField] private AccountDetailPage accountDetailPage;

    private void Awake()
    {
        rankingList.OnClickDetail.AddListener(accountDetailPage.SetAccountSoData);
        friendList.OnClickDetail.AddListener(accountDetailPage.SetAccountSoData);
        addFriendHandler.OnClickAvatarEvent.AddListener(accountDetailPage.SetAccountSoData);
    }

    private void OnValidate()
    {
        btnReturn ??= transform.Find("Button��^").GetComponent<Button>();
        rankingList ??= transform.Find("�Ʀ�]�C��").GetComponent<RankingList>();
        friendList ??= transform.Find("�n�ͦW��C��").GetComponent<FriendList>();
        addFriendHandler ??= transform.Find("�s�W�n�ͭ���").GetComponent<AddFriendHandler>();
        accountDetailPage ??= transform.Find("�b���ԲӸ�T").GetComponent<AccountDetailPage>();
    }
}
