using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// �����N - ���ꭶ�� (�峹�B���`)
/// </summary>
public class Page_UnlockArticle : MonoBehaviour
{
    [SerializeField] private SO_Article soArticle;
    [SerializeField] private SO_ArticleChapter soArticleChapter;

    [Header(">>> UI�ե�")]
    [SerializeField] private TextMeshProUGUI txtTitle;
    [SerializeField] private TextMeshProUGUI txtCost;
    [SerializeField] private Button btnConfirm;
    [SerializeField] private Button btnWatchAD;

    [Header(">>>�I���T�{�����")]
    public UnityEvent<SO_Article> onUnlockArticle;
    public UnityEvent<SO_ArticleChapter> onUnlockChapter;

    [Header(">>>�I���ݼs�i���s��")]
    public UnityEvent<SO_Article> onClickWatchAD_Article;
    public UnityEvent<SO_ArticleChapter> onClickWatchAD_Chapter;


    private void Awake()
    {
        btnConfirm.onClick.AddListener(onClickConfirm);
        btnWatchAD.onClick.AddListener(onClickWatchAD);
    }

    private void onClickConfirm()
    {
        if (soArticle != null) onUnlockArticle?.Invoke(soArticle);
        else if (soArticleChapter != null) onUnlockChapter?.Invoke(soArticleChapter);
    }

    private void onClickWatchAD()
    {
        if (soArticle != null) onClickWatchAD_Article?.Invoke(soArticle);
        else if (soArticleChapter != null) onClickWatchAD_Chapter?.Invoke(soArticleChapter);
    }

    public void SetUnlockItem(SO_Article articleData)
    {
        soArticle = articleData;
        soArticleChapter = null;

        RefreshData(articleData.ArticleTitle, articleData.WakePointCost);
    }

    public void SetUnlockItem(SO_ArticleChapter articleChapterData)
    {
        soArticleChapter = articleChapterData;
        soArticle = null;

        RefreshData(articleChapterData.ChapterTitle, articleChapterData.WakePointCost);
    }

    private void RefreshData(string title, int wakePointCost)
    {
        txtTitle.SetText(title);
        txtCost.SetText($"��{wakePointCost}�I�����I����?");
        gameObject.SetActive(true);
    }
}
