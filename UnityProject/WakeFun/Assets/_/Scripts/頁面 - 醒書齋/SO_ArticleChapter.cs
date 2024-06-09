using UnityEngine;

/// <summary>
/// �����N - ���`SO���
/// </summary>
public class SO_ArticleChapter : ScriptableObject
{
    [Header(">>> ���`���D")]
    [SerializeField] private string chatperTitle;

    [Header(">>> ���`���e ")]
    [SerializeField] private string content;

    [Header(">>> ���`���O�I��")]
    [SerializeField] private int wakePointCost;

    [Header(">>> �O�_�w����")]
    [SerializeField] private bool isUnlock;

    public string ChapterTitle => chatperTitle;
    public string Content => content;
    public int WakePointCost => wakePointCost;

    public bool IsUnLock => isUnlock;

    /// <summary>
    /// �]�m���e
    /// </summary>
    public void SetData(string chatperTitle, string content, int wakePointCost, bool isUnlock)
    {
        this.chatperTitle = chatperTitle;
        this.content = content;
        this.wakePointCost = wakePointCost;
        this.isUnlock = isUnlock;
    }
}
