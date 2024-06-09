using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 醒書齋 - 章節列表項目
/// </summary>
[RequireComponent(typeof(Button))]
public class ArticleCharpterList_Item : MonoBehaviour
{
    [Header(">>> 醒書齋 - 章節SO資料")]
    [SerializeField] private SO_ArticleChapter soCharterData;

    [Header(">>> UI組件")]
    [SerializeField] private Button btnDetail;
    [SerializeField] private TextMeshProUGUI txtChapterTitle;
    [SerializeField] private Image imgLock;

    public SO_ArticleChapter soArticleChapterData
    {
        set
        {
            soCharterData = value;
            txtChapterTitle.SetText(soCharterData.ChapterTitle);
            imgLock.gameObject.SetActive(soCharterData.IsUnLock == false);
        }
    }

    /// <summary>
    /// 事件：點擊本體
    /// </summary>
    public UnityEvent<SO_ArticleChapter> onClickDetailButton;

    private void Awake()
    {
        btnDetail.onClick.AddListener(() => onClickDetailButton?.Invoke(soCharterData));
    }

    private void OnValidate()
    {
        btnDetail ??= GetComponent<Button>();
        txtChapterTitle ??= transform.Find("txtChapterTitle").GetComponent<TextMeshProUGUI>();
        imgLock ??= transform.Find("imgLock").GetComponent<Image>();
    }

}
