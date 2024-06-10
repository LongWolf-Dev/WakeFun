using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �����ưʦC�� - �Ʀ�]
/// </summary>
public class RankingList : ScrollRectList<RankingList_Item, SO_Account>
{
    [Header(">>> ��C���� - �s�W�n�� �Q�I���ɵo�e�ƥ�")]
    public UnityEvent<SO_Account> OnClickAddFriend;

    protected override void AddActionInSetDataListForLoop(RankingList_Item item, SO_Account data)
    {
        item.achievementSO = data;
        item.OnClickDetailButton.AddListener((soData) => onItemClicked.Invoke(soData));
        item.OnClickAddFriendButton.AddListener((soData) => OnClickAddFriend.Invoke(soData));
    }
}
