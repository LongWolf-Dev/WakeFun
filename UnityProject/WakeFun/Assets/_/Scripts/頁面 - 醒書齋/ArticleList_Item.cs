using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 醒書齋 - 文章列表項目
/// </summary>
[RequireComponent(typeof(Button))]
public class ArticleList_Item : MonoBehaviour
{
    [Header(">>> 醒書齋 - 文章SO資料")]
    [SerializeField] private SO_Article soArticle;

    [Header(">>> UI組件")]
    [SerializeField] private Button btnDetail;
    [SerializeField] private TextMeshProUGUI txtArticleTitle;
    [SerializeField] private Image imgLock;

    public SO_Article soArticleData
    {
        set
        {
            soArticle = value;
            RefreshData();
        }
    }

    /// <summary>
    /// 事件：點擊本體
    /// </summary>
    public UnityEvent<SO_Article> onClickDetailButton;

    private void Awake()
    {
        btnDetail.onClick.AddListener(() => onClickDetailButton?.Invoke(soArticle));
    }

    private void OnValidate()
    {
        btnDetail ??= GetComponent<Button>();
        txtArticleTitle ??= transform.Find("txtArticleTitle").GetComponent<TextMeshProUGUI>();
        imgLock ??= transform.Find("imgLock").GetComponent<Image>();
        RefreshData();
    }

    private void RefreshData()
    {
        if (soArticle == null)
        {
            soArticle = ScriptableObject.CreateInstance<SO_Article>();
            soArticle._SetupRandomData();
        }
        txtArticleTitle.SetText(soArticle.ArticleTitle);
        imgLock.gameObject.SetActive(soArticle.IsUnlock == false);
    }
}
