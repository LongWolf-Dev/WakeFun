using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �����ưʦC�� - �n�ͦW��
/// </summary>
public class FriendList : ScrollRectList<FriendList_Item, SO_Account>
{
    [Header(">>> ��C���� - Toggle�g �Q�I���ɵo�e�ƥ�")]
    public UnityEvent<SO_Account, bool> OnClickLikedToggle;

    protected override void AddActionInSetDataListForLoop(FriendList_Item item, SO_Account data)
    {
        item.AccountSO = data;
        item.OnClickDetailButton.AddListener((soData) => onItemClicked.Invoke(soData));
        item.OnClickLikedToggle.AddListener((soData, isOn) => OnClickLikedToggle.Invoke(soData, isOn));
    }
}
