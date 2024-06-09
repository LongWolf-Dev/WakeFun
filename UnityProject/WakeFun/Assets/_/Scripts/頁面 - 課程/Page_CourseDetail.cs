using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 頁面 - 課程簡介
/// </summary>
public class Page_CourseDetail : MonoBehaviour
{
    [Header(">>> 課程SO")]
    [SerializeField] private SO_Course soCourse;

    [Header(">>> UI組件")]
    [SerializeField] private TextMeshProUGUI txtCourseTitle;
    [SerializeField] private TextMeshProUGUI txtCost, txtDescription, txtMoreInfoMail;
    [SerializeField] private TextMeshProUGUI txtTeacherName, txtDate, txtLocation;
    [SerializeField] private Button btnUrlSignUp, btnUrlTable;
    [SerializeField] private Button btnPoster;

    public void SetCourseData(SO_Course courseData)
    {
        soCourse = courseData;
        RefreshData();
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        btnUrlSignUp.onClick.AddListener(() => OnClickLink(btnUrlSignUp));
        btnUrlTable.onClick.AddListener(() => OnClickLink(btnUrlTable));
        btnPoster.onClick.AddListener(() => OnClickLink(btnPoster));
    }

    private void OnClickLink(Button btn)
    {
        if (btn == btnUrlSignUp)
        {
            //官網報名連結
        }
        else if (btn == btnUrlTable)
        {
            //表單連結
        }
        else if (btn == btnPoster)
        {
            //課程海報
        }
    }

    private void OnValidate()
    {
        Transform panel = transform.GetChild(2);

        txtCourseTitle ??= panel.transform.Find("txtCourseTitle").GetComponent<TextMeshProUGUI>();
        txtTeacherName ??= panel.transform.Find("txtTeacherName").GetComponent<TextMeshProUGUI>();
        txtDate ??= panel.transform.Find("txtDate").GetComponent<TextMeshProUGUI>();
        txtLocation ??= panel.transform.Find("txtLocation").GetComponent<TextMeshProUGUI>();

        txtCost ??= panel.transform.Find("txtCost").GetComponent<TextMeshProUGUI>();
        txtDescription ??= panel.transform.Find("txtDescription").GetComponent<TextMeshProUGUI>();
        txtMoreInfoMail ??= panel.transform.Find("txtMoreInfoMail").GetComponent<TextMeshProUGUI>();

        btnUrlSignUp ??= panel.transform.Find("btnUrlSignUp").GetComponent<Button>();
        btnUrlTable ??= panel.transform.Find("btnUrlTable").GetComponent<Button>();
        btnPoster ??= panel.transform.Find("btnPoster").GetComponent<Button>();
    }

    private void RefreshData()
    {
        txtCourseTitle.SetText(soCourse.CourseTitle);
        txtTeacherName.SetText(soCourse.TeacherName);
        txtDate.SetText(soCourse.Date);
        txtLocation.SetText(soCourse.Location);

        txtCost.SetText($"${soCourse.Cost}元");
        txtDescription.SetText(soCourse.Description);
        txtMoreInfoMail.SetText(soCourse.MoreInfomationMail);

        btnUrlSignUp.gameObject.SetActive(string.IsNullOrEmpty(soCourse.Url_SignUp) == false);
        btnUrlTable.gameObject.SetActive(string.IsNullOrEmpty(soCourse.Url_Table) == false);
        btnPoster.gameObject.SetActive(string.IsNullOrEmpty(soCourse.Url_Poster) == false);
    }
}
