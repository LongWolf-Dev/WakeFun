using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 課程列表項目
/// </summary>
[RequireComponent(typeof(Button))]
public class CourseList_Item : MonoBehaviour
{
    [Header(">>> 課程SO")]
    [SerializeField] private SO_Course soCourse;

    [Header(">>> 當點擊課程項目時")]
    public UnityEvent<SO_Course> onClickCourse;

    [Header(">>> UI組件")]
    [SerializeField] private Button btnDetail;
    [SerializeField] private TextMeshProUGUI txtCourseTitle, txtTeacherName;

    private void Awake()
    {
        btnDetail.onClick.AddListener(() => onClickCourse?.Invoke(soCourse));
    }

    public SO_Course soCourseData
    {
        set
        {
            soCourse = value;
            RefreshData();
        }
    }

    private void OnValidate()
    {
        btnDetail ??= GetComponent<Button>();
        txtCourseTitle ??= transform.Find("txtCourseTitle").GetComponent<TextMeshProUGUI>();
        txtTeacherName ??= transform.Find("txtTeacherName").GetComponent<TextMeshProUGUI>();
        RefreshData();
    }
    private void RefreshData()
    {
        if (soCourse == null)
        {
            soCourse = ScriptableObject.CreateInstance<SO_Course>();
            soCourse._SetupRandomData();
        }
        txtCourseTitle.SetText(soCourse.CourseTitle);
        txtTeacherName.SetText(soCourse.TeacherName);
    }
}
