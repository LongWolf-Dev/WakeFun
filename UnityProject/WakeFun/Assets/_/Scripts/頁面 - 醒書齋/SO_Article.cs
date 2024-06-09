using UnityEngine;
using VictorDev.WakeFun;

/// <summary>
/// �����N - �峹SO���
/// </summary>
public class SO_Article : ScriptableObject
{
    [Header(">>> �����N�峹���D")]
    [SerializeField] private string articleTitle;

    [Header(">>> �峹���O�I��")]
    [SerializeField] private int wakePointCost;

    [Header(">>> �O�_�w����")]
    [SerializeField] private bool isUnlock;

    public string ArticleTitle => articleTitle;
    public int WakePointCost => wakePointCost;
    public bool IsUnlock => isUnlock;

    /// <summary>
    /// �s�@�üƸ�� (For���ե�)
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
