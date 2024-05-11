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
    [SerializeField] private int fee;

    [Header(">>> 課程說明 ")]
    [TextArea(3, 10)]
    [SerializeField] private string description;
    [Header(">>> 更多資訊")]
    [SerializeField] private string moreInfomation;
    [Header(">>> 海報")]
    [SerializeField] private Sprite poster;

    private void OnValidate() => SetupRandom();
    private void SetupRandom()
    {
        if (string.IsNullOrEmpty(courseTitle))
        {
            string[] courseInfo = RandomDataUtils.GetCourseByRandom();
            courseTitle = courseInfo[0];
            description = courseInfo[1];
        }
        if (string.IsNullOrEmpty(teacherName)) teacherName = RandomDataUtils.GetNameByRandom();
        if (string.IsNullOrEmpty(date)) date = RandomDataUtils.GetRandomDateTimeAfterMonthsToString();
        if (fee == 0) fee = Random.Range(2, 37 + 1) * 100;
        if (string.IsNullOrEmpty(location))
        {
            string[] address = RandomDataUtils.GetAddressByRandom();
            location = address[0] + address[1];
        }
        if (string.IsNullOrEmpty(moreInfomation)) moreInfomation = RandomDataUtils.GetEMailByRandom();
    }
}