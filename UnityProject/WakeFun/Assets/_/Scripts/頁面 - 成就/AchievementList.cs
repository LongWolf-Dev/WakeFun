using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 垂直滑動列表 - 成就
/// </summary>
public class AchievementList : ScrollRectList<AchievementList_Item, SO_Achievement>
{
    [Header(">>> 當列表項目 - 領取獎勵 被點擊時發送事件")]
    public UnityEvent<SO_Achievement> OnClickReceivedReward;

    private void Start()
    {
        foreach (Transform child in scrollRectContainer.transform)
        {
            SetEventListener(child.GetComponent<AchievementList_Item>());
        }
    }

    protected override void AddActionInSetDataListForLoop(AchievementList_Item item, SO_Achievement data)
    {
        item.AchievementSO = data;
        SetEventListener(item);
    }

    private void SetEventListener(AchievementList_Item item)
    {
        item.OnClickDetail.AddListener((soData) => onItemClicked.Invoke(soData));
        item.OnClickReceivedReward.AddListener((soData) => OnClickReceivedReward.Invoke(soData));
    }
}
