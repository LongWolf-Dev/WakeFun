using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 垂直滑動列表 - 好友名單
/// </summary>
public class FriendList : ScrollRectList<FriendList_Item, SO_Account>
{
    [Header(">>> 當列表項目 - Toggle讚 被點擊時發送事件")]
    public UnityEvent<SO_Account, bool> OnClickLikedToggle;

    protected override void AddActionInSetDataListForLoop(FriendList_Item item, SO_Account data)
    {
        item.AccountSO = data;
        item.OnClickDetailButton.AddListener((soData) => onItemClicked.Invoke(soData));
        item.OnClickLikedToggle.AddListener((soData, isOn) => OnClickLikedToggle.Invoke(soData, isOn));
    }
}
