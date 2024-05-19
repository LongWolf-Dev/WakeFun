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
        if (toggleIsHaveRewared != null)
        {
            toggleIsHaveRewared.onValueChanged.AddListener((isOn) =>
            {
                if (isOn == false)
                {
                    OnClickReceivedReward?.Invoke(achievementSO);
                    toggleIsHaveRewared.interactable = false;
                }
            });
        }
    }
    private void OnValidate()
    {
        btnDetail ??= GetComponent<Button>();
        txtTitle ??= transform.Find("txt標題").GetComponent<Text>();
        RefreshData();
    }

    private void RefreshData()
    {
        if (achievementSO == null)
        {
            achievementSO = ScriptableObject.CreateInstance<SO_Achievement>();
            achievementSO._SetupRandomData();
        }

        txtTitle.text = achievementSO.Title;

        if (txtDescription != null) txtDescription.text = achievementSO.Description;
        if (toggleIsHaveRewared != null)
        {
            toggleIsHaveRewared.isOn = achievementSO.IsHaveReward;
            toggleIsHaveRewared.interactable = achievementSO.IsHaveReward;
        }

        if (txtAchieveTimeStamp != null) txtAchieveTimeStamp.text = achievementSO.AchieveDateTime;
    }
}
