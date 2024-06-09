using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 醒書齋 - 解鎖頁面 (文章、章節)
/// </summary>
public class Page_UnlockArticle : MonoBehaviour
{
    [SerializeField] private SO_Article soArticle;
    [SerializeField] private SO_ArticleChapter soArticleChapter;

    [Header(">>> UI組件")]
    [SerializeField] private TextMeshProUGUI txtTitle;
    [SerializeField] private TextMeshProUGUI txtCost;
    [SerializeField] private Button btnConfirm;
    [SerializeField] private Button btnWatchAD;

    [Header(">>>點擊確認解鎖時")]
    public UnityEvent<SO_Article> onUnlockArticle;
    public UnityEvent<SO_ArticleChapter> onUnlockChapter;

    [Header(">>>點擊看廣告按鈕時")]
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
        txtCost.SetText($"用{wakePointCost}點醒樂點解鎖?");
        gameObject.SetActive(true);
    }
}
