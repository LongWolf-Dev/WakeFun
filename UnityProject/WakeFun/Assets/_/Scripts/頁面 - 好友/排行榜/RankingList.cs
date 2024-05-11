using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 垂直滑動列表 - 排行榜
/// </summary>
public class RankingList : ScrollRectList<RankingList_Item, SO_Account>
{
    [Header(">>> 當列表項目被點擊時發送事件")]
    public UnityEvent<SO_Account> OnClickDetail = new UnityEvent<SO_Account>();

    [Header(">>> 當列表項目 - 新增好友 被點擊時發送事件")]
    public UnityEvent<SO_Account> OnClickAddFriend = new UnityEvent<SO_Account>();

    protected override void AddActionInSetDataListForLoop(RankingList_Item item, SO_Account data)
    {
        item.AccountSO = data;
        item.OnClickDetailButton.AddListener((soData) => OnClickDetail.Invoke(soData));
        item.OnClickAddFriendButton.AddListener((soData) => OnClickAddFriend.Invoke(soData));
    }

    protected override void ToClearList()
    {
    }
}
