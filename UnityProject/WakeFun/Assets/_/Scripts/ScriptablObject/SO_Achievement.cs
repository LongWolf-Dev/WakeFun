using System;
using UnityEngine;
using VictorDev.WakeFun;
using Random = UnityEngine.Random;

/// <summary>
/// 成就 SO
/// </summary>
[CreateAssetMenu(fileName = "SO_成就資料", menuName = ">>WakeFun<</ScriptableObject/SO_成就資料")]
public class SO_Achievement : ScriptableObject
{
    [Header(">>> 成就標題")]
    [SerializeField] private string title;
    [Header(">>> 成就說明 ")]
    [SerializeField] private string description;
    [Header(">>> 是否可以領取獎勵")]
    [SerializeField] private bool isHaveRewared;
    [Header(">>> 是否達成")]
    [SerializeField] private bool isAchieved;
    [Header(">>> 完成成就時間點")]
    [SerializeField] private DateTime achieveTimeStamp;

    #region [>>> Getter]
    /// <summary>
    /// 成就標題
    /// </summary>
    public string Title => title;
    /// <summary>
    /// 成就說明
    /// </summary>
    public string Description => description;
    /// <summary>
    /// 是否已領取獎勵
    /// </summary>
    public bool IsHaveReward => isHaveRewared;
    /// <summary>
    /// 是否達成
    /// </summary>
    public bool IsAchieved => isAchieved;
    /// <summary>
    /// 完成成就時間點
    /// </summary>
    public string AchieveDateTime => achieveTimeStamp.ToString("yyyy/MM/dd tt h:mm");
    #endregion

    /// <summary>
    /// 製作亂數資料 (For測試用)
    /// </summary>
    public void _SetupRandomData()
    {
        if (string.IsNullOrEmpty(title))
        {
            string[] tempAchievement = RandomDataUtils.GetAchievementByRandom();
            title = tempAchievement[0];
            description = tempAchievement[1];
            isHaveRewared = Random.Range(0, 11) == 10;
        }
    }
}