using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 列表組件Item - 成就
/// </summary>
[RequireComponent(typeof(Button))]
public class AchievementList_Item : MonoBehaviour
{
    [Header(">>> 列表組件Item - 成就")]
    [SerializeField] private SO_Achievement achievementSO;

    [SerializeField] private Button btnDetail;
    [SerializeField] private Text txtTitle;

    [SerializeField] private Toggle toggleIsHaveRewared;
    [SerializeField] private Text txtDescription;
    [SerializeField] private Text txtAchieveTimeStamp;

    /// <summary>
    /// 事件：點擊項目
    /// </summary>
    public UnityEvent<SO_Achievement> OnClickDetail;

    /// <summary>
    /// 事件：點擊領取獎勵
    /// </summary>
    public UnityEvent<SO_Achievement> OnClickReceivedReward;

    public SO_Achievement AchievementSO
    {
        set
        {
            achievementSO = value;
            RefreshData();
        }
    }

    private void Awake()
    {
        btnDetail.onClick.AddListener(() => OnClickDetail.Invoke(achievementSO));
        toggleIsHaveRewared.onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                OnClickReceivedReward?.Invoke(achievementSO);
                toggleIsHaveRewared.interactable = false;
            }
        });
    }
    private void OnValidate()
    {
        btnDetail ??= GetComponent<Button>();
        txtTitle ??= transform.Find("txt標題").GetComponent<Text>();
        RefreshData();
    }

    private void RefreshData()
    {
        SO_Achievement data;
        if (achievementSO != null) data = achievementSO;
        else
        {
            SO_Achievement tempData = ScriptableObject.CreateInstance<SO_Achievement>();
            tempData._SetupRandomData();
            data = tempData;
        }


        txtTitle.text = data.Title;

        if (txtDescription != null) txtDescription.text = data.Description;
        if (toggleIsHaveRewared != null)
        {
            toggleIsHaveRewared.isOn = data.IsHaveReward;
            toggleIsHaveRewared.interactable = data.IsHaveReward;
        }

        if (txtAchieveTimeStamp != null) txtAchieveTimeStamp.text = data.AchieveDateTime;
    }
}
