using UnityEngine;

/// <summary>
/// 醒書齋 - 章節SO資料
/// </summary>
public class SO_ArticleChapter : ScriptableObject
{
    [Header(">>> 章節標題")]
    [SerializeField] private string chatperTitle;

    [Header(">>> 章節內容 ")]
    [SerializeField] private string content;

    [Header(">>> 章節消費點數")]
    [SerializeField] private int wakePointCost;

    [Header(">>> 是否已解鎖")]
    [SerializeField] private bool isUnlock;

    public string ChapterTitle => chatperTitle;
    public string Content => content;
    public int WakePointCost => wakePointCost;

    public bool IsUnLock => isUnlock;

    /// <summary>
    /// 設置內容
    /// </summary>
    public void SetData(string chatperTitle, string content, int wakePointCost, bool isUnlock)
    {
        this.chatperTitle = chatperTitle;
        this.content = content;
        this.wakePointCost = wakePointCost;
        this.isUnlock = isUnlock;
    }
}
