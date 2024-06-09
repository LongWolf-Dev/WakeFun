using UnityEngine;
using VictorDev.WakeFun;

/// <summary>
/// 醒書齋 - 文章SO資料
/// </summary>
public class SO_Article : ScriptableObject
{
    [Header(">>> 醒樂齋文章標題")]
    [SerializeField] private string articleTitle;

    [Header(">>> 文章消費點數")]
    [SerializeField] private int wakePointCost;

    [Header(">>> 是否已解鎖")]
    [SerializeField] private bool isUnlock;

    public string ArticleTitle => articleTitle;
    public int WakePointCost => wakePointCost;
    public bool IsUnlock => isUnlock;

    /// <summary>
    /// 製作亂數資料 (For測試用)
    /// </summary>
    public void _SetupRandomData()
    {
        if (string.IsNullOrEmpty(articleTitle))
        {
            articleTitle = RandomDataUtils.GetRandomArticle();
            wakePointCost = Random.Range(1, 4) * 10;
            isUnlock = Random.Range(0, 11) >= 5;
        }
    }
}
