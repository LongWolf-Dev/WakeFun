using UnityEngine;
using VictorDev.EditorTool;

/// <summary>
/// 課程資料
/// </summary>
[CreateAssetMenu(fileName = "SO_課程資料", menuName = ">>WakeFun<</ScriptableObject/SO_課程資料")]
public class SO_Course : ScriptableObject
{
    [Header(">>> 課程名稱")]
    [SerializeField] private string courseTitle;
    [Header(">>> 老師名稱")]
    [SerializeField] private string teacherName;
    [Header(">>> 日期時間")]
    [SerializeField] private string date;
    [Header(">>> 上課地點")]
    [SerializeField] private string location;
    [Header(">>> 課程費用")]
    [SerializeField] private int cost;

    [Header(">>> 課程說明 ")]
    [TextArea(3, 10)]
    [SerializeField] private string description;
    [Header(">>> 更多資訊")]
    [SerializeField] private string moreInfomationMail;
    [Header(">>> 海報")]
    [SerializeField] private string url_Poster;
    [Header(">>> 官網報名鏈結")]
    [SerializeField] private string url_SignUp;
    [Header(">>> 表單鏈結")]
    [SerializeField] private string url_Table;

    #region [>>> Getter]
    public string CourseTitle => courseTitle;
    public string TeacherName => teacherName;
    public string Date => date;
    public string Location => location;
    public int Cost => cost;
    public string Description => description;
    public string MoreInfomationMail => moreInfomationMail;
    public string Url_Poster => url_Poster;
    public string Url_SignUp => url_SignUp;
    public string Url_Table => url_Table;
    #endregion

    public void SetData(string courseTitle, string teacherName, string date, string location, int cost, string description, string moreInfomationMail, string imgPost, string url_SignUp, string url_Table)
    {
        this.courseTitle = courseTitle;
        this.teacherName = teacherName;
        this.date = date;
        this.location = location;
        this.cost = cost;
        this.description = description;
        this.moreInfomationMail = moreInfomationMail;
        this.url_Poster = imgPost;
        this.url_SignUp = url_SignUp;
        this.url_Table = url_Table;
    }

    /// <summary>
    /// 製作亂數資料 (For測試用)
    /// </summary>
    public void _SetupRandomData()
    {
        if (string.IsNullOrEmpty(courseTitle))
        {
            string[] courseInfo = RandomDataUtils.GetCourseByRandom();
            courseTitle = courseInfo[0];
            description = courseInfo[1];

            teacherName = RandomDataUtils.GetNameByRandom();
            date = RandomDataUtils.GetRandomDateTimeAfterMonthsToString();
            cost = Random.Range(2, 21) * 100;

            string[] address = RandomDataUtils.GetAddressByRandom();
            location = address[0] + address[1];

            moreInfomationMail = RandomDataUtils.GetEMailByRandom();
        }
    }
}