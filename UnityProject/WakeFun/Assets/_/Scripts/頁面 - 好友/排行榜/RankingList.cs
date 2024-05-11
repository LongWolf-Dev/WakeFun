using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �����ưʦC�� - �Ʀ�]
/// </summary>
public class RankingList : ScrollRectList<RankingList_Item, SO_Account>
{
    [Header(">>> ��C���سQ�I���ɵo�e�ƥ�")]
    public UnityEvent<SO_Account> OnClickDetail = new UnityEvent<SO_Account>();

    [Header(">>> ��C���� - �s�W�n�� �Q�I���ɵo�e�ƥ�")]
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
