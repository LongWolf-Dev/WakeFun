using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VictorDev.WakeFun;

/// <summary>
/// 頁面 - 醒書齋 文章列表
/// </summary>
public class Page_Article : ScrollRectList<ArticleList_Item, SO_Article>
{
    [Header(">>> 醒樂點數量")]
    [SerializeField] private TextMeshProUGUI txtWakeFunPoint;

    [Header(">>> 頁面 - 章節列表")]
    [SerializeField] private Page_ArticleCharpter pageChapterList;

    [Header(">>> 頁面 - 章節內容")]
    [SerializeField] private Page_ChapterContent pageChapterContent;

    [Header(">>> 頁面 - 購買解鎖視窗")]
    [SerializeField] private Page_UnlockArticle pageUnlockArticle;

    private void Awake()
    {
        pageChapterList.onItemClicked.AddListener(OnClickDetail);

        //ForDemo
        foreach (Transform child in scrollRectContainer.transform)
        {
            child.GetComponent<ArticleList_Item>().onClickDetailButton.AddListener(OnClickDetail);
        }
    }

    protected override void AddActionInSetDataListForLoop(ArticleList_Item item, SO_Article soData)
    {
        item.soArticleData = soData;
        item.onClickDetailButton.AddListener(OnClickDetail);
    }

    /// <summary>
    /// 當點擊文章項目時，向WebAPI取得資料
    /// </summary>
    private void OnClickDetail(SO_Article articleData)
    {
        if (articleData.IsUnlock)
        {
            //取得隨機資料ForDemo
            Dictionary<string, string> tempChapterList = RandomDataUtils.GetRandomChapter(articleData.ArticleTitle);
            List<SO_ArticleChapter> chapterList = new List<SO_ArticleChapter>();
            foreach (string chapterTitle in tempChapterList.Keys)
            {
                SO_ArticleChapter soChapter = ScriptableObject.CreateInstance<SO_ArticleChapter>();
                soChapter.SetData(chapterTitle, tempChapterList[chapterTitle], Random.Range(1, 4) * 10, Random.Range(1, 10) > 5);
                chapterList.Add(soChapter);
            }
            pageChapterList.SetDataList(chapterList);
            pageChapterList.soArticleData = articleData;

            pageChapterList.gameObject.SetActive(true);
        }
        else pageUnlockArticle.SetUnlockItem(articleData);
    }

    /// <summary>
    /// 當點擊章節項目時
    /// </summary>
    private void OnClickDetail(SO_ArticleChapter chapterData)
    {
        if (chapterData.IsUnLock) pageChapterContent.soChapterData = chapterData;
        else pageUnlockArticle.SetUnlockItem(chapterData);
    }
}
