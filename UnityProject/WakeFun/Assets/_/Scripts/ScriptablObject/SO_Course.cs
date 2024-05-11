using UnityEngine;
using VictorDev.EditorTool;

/// <summary>
/// �ҵ{���
/// </summary>
[CreateAssetMenu(fileName = "SO_�ҵ{���", menuName = ">>WakeFun<</ScriptableObject/SO_�ҵ{���")]
public class SO_Course : ScriptableObject
{
    [Header(">>> �ҵ{�W��")]
    [SerializeField] private string courseTitle;
    [Header(">>> �Ѯv�W��")]
    [SerializeField] private string teacherName;
    [Header(">>> ����ɶ�")]
    [SerializeField] private string date;
    [Header(">>> �W�Ҧa�I")]
    [SerializeField] private string location;
    [Header(">>> �ҵ{�O��")]
    [SerializeField] private int fee;

    [Header(">>> �ҵ{���� ")]
    [TextArea(3, 10)]
    [SerializeField] private string description;
    [Header(">>> ��h��T")]
    [SerializeField] private string moreInfomation;
    [Header(">>> ����")]
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