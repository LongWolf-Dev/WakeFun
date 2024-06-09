using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� - �����N ���`����
/// </summary>
public class Page_ChapterContent : MonoBehaviour
{
    [SerializeField] private SO_ArticleChapter soArticleChapter;
    [Header(">>> UI�ե�")]
    [SerializeField] private TextMeshProUGUI txtChapterTitle;
    [SerializeField] private TextMeshProUGUI txtChapterContent;
    [SerializeField] private ScrollRect scrollRectContent;


    public SO_ArticleChapter soChapterData
    {
        set
        {
            soArticleChapter = value;
            gameObject.SetActive(true);
            txtChapterTitle.SetText(soArticleChapter.ChapterTitle);
            txtChapterContent.SetText(soArticleChapter.Content);
            scrollRectContent.verticalNormalizedPosition = 1;
        }
    }
}
