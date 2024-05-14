using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �����ưʦC�� - �Ʀ�]
/// </summary>
public class RankingList : ScrollRectList<RankingList_Item, SO_Account>
{
    [Header(">>> ��C���سQ�I���ɵo�e�ƥ�")]
    public UnityEvent<SO_Account> OnClickDetail;

    [Header(">>> ��C���� - �s�W�n�� �Q�I���ɵo�e�ƥ�")]
    public UnityEvent<SO_Account> OnClickAddFriend;

    protected override void AddActionInSetDataListForLoop(RankingList_Item item, SO_Account data)
    {
        item.AccountSO = data;
        item.OnClickDetailButton.AddListener((soData) => OnClickDetail.Invoke(soData));
        item.OnClickAddFriendButton.AddListener((soData) => OnClickAddFriend.Invoke(soData));
    }

    protected override void ToClearList()
    {
    }

    /// <summary>
    /// For Test
    /// </summary>
    private void Awake()
    {
        if(scrollView.content.childCount >= 0)
        {
            foreach (Transform child in scrollView.content.transform)
            {
                RankingList_Item item = child.GetComponent<RankingList_Item>();
                item.OnClickDetailButton.AddListener((soData) => OnClickDetail.Invoke(soData));
                item.OnClickAddFriendButton.AddListener((soData) => OnClickAddFriend.Invoke(soData));
            }
        }
    }
}
