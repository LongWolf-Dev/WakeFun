using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageHandler_Friend : MonoBehaviour
{
    [Header(">>> 返回鈕")]
    [SerializeField] private Button btnReturn;

    [Header(">>> 排行榜列表")]
    [SerializeField] private RankingList rankingList;
    [Header(">>> 好友名單列表")]
    [SerializeField] private FriendList friendList;
    [Header(">>> 新增好友頁面")]
    [SerializeField] private AddFriendHandler addFriendHandler;

    private void OnValidate()
    {
        btnReturn ??= transform.Find("Button返回").GetComponent<Button>();
        rankingList ??= transform.Find("排行榜列表").GetComponent<RankingList>();
        friendList ??= transform.Find("好友名單列表").GetComponent<FriendList>();
        addFriendHandler ??= transform.Find("新增好友頁面").GetComponent<AddFriendHandler>();
    }
}
