using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 頁面主體 - 成就
/// </summary>
public class Page_Achievement : MonoBehaviour
{
    [Header(">>> 返回鈕")]
    [SerializeField] private Button btnReturn;

    [Header(">>> 成就列表")]
    [SerializeField] private AchievementList achievementList;
    [Header(">>> 歷史成就列表")]
    [SerializeField] private AchievementList achievemenHistoryList;

    [Header(">>> 頁面 - 獲得成就資訊UI")]
    [SerializeField] private GameObject rewardDetailPage;
    [SerializeField] private Text txtAchieveTitle, txtName, txtReward;
    [SerializeField] private Button btnLine;

    private void Awake()
    {
        achievementList.OnClickReceivedReward.AddListener(ToReceivedReward);
        achievementList.onItemClicked.AddListener(OnItemClicked);
        achievemenHistoryList.onItemClicked.AddListener(OnItemClicked);

        btnLine.onClick.AddListener(null);
    }

    /// <summary>
    /// 點擊選單項目時
    /// </summary>
    private void OnItemClicked(SO_Achievement soAchievementData)
    {
        Debug.Log($"OnItemClicked: {soAchievementData.Title}");
    }

    /// <summary>
    /// 點擊領取獎勵
    /// </summary>
    private void ToReceivedReward(SO_Achievement soAchievementData)
    {
        Debug.Log($"ToReceivedReward: {soAchievementData.Title}");
        rewardDetailPage.SetActive(true);
        txtAchieveTitle.text = soAchievementData.Title;
    }


    private void OnValidate()
    {
        btnReturn ??= transform.GetChild(0).Find("Button返回").GetComponent<Button>();
        achievementList ??= transform.Find("成就列表").GetComponent<AchievementList>();
        achievemenHistoryList ??= transform.Find("歷史成就列表").GetComponent<AchievementList>();
    }
}
