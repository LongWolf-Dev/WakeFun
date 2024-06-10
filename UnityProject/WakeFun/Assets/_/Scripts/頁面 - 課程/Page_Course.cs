using UnityEngine;
using UnityEngine.UI;

public class Page_Course : ScrollRectList<CourseList_Item, SO_Course>
{
    [Header(">>> 頁面-課程簡介")]
    [SerializeField] private Page_CourseDetail pageCourseDetail;
    [Header(">>> 聯絡醒樂LINE   ")]
    [SerializeField] private Button btnContectLine;

    protected override void AddActionInSetDataListForLoop(CourseList_Item item, SO_Course soData)
    {
        item.soCourseData = soData;
        item.onClickCourse.AddListener(pageCourseDetail.SetCourseData);
    }

    private void OnValidate()
    {
        btnContectLine ??= transform.Find("Button_ContectLine").GetComponent<Button>();
    }
}
