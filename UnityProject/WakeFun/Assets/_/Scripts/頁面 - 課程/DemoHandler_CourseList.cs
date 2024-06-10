using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Demo產生資料用
/// </summary>
public class DemoHandler_CourseList : MonoBehaviour
{
    [SerializeField] private int numOfData = 15;
    [SerializeField] private Page_Course pageCourse;

    private void Start() => CreateData();

    [ContextMenu("- Create Data")]
    private void CreateData()
    {
        List<SO_Course> courseList = new List<SO_Course>();

        for (int i = 0; i < numOfData; i++)
        {
            SO_Course course = ScriptableObject.CreateInstance<SO_Course>();
            course._SetupRandomData();
            courseList.Add(course);
        }
        pageCourse.SetDataList(courseList);
    }

    private void OnValidate()
    {
        name = "_DemoHandler_Course";
        pageCourse ??= transform.parent.GetComponent<Page_Course>();
    }
}
