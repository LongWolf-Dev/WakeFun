using TMPro;
using UnityEngine;
/// <summary>
///  頁面：醒書齋 - 章節列表
/// </summary>
public class Page_ArticleCharpter : ScrollRectList<ArticleCharpterList_Item, SO_ArticleChapter>
{
    [SerializeField] private SO_Article soArticle;
    [SerializeField] private TextMeshProUGUI txtArticleTitle;

    public SO_Article soArticleData
    {
        set
        {
            soArticle = value;
            txtArticleTitle.SetText(soArticle.ArticleTitle);
        }
    }

    protected override void AddActionInSetDataListForLoop(ArticleCharpterList_Item item, SO_ArticleChapter soData)
    {
        item.soArticleChapterData = soData;
        item.onClickDetailButton.AddListener(onItemClicked.Invoke);
    }
}
