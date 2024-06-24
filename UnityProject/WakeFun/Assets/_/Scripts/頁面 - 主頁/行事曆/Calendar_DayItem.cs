using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Calendar_DayItem : MonoBehaviour
{
    [Header(">>> 點選後發出事件")]
    public UnityEvent<DateTime> onClick;

    [Header(">>> UI組件")]
    [SerializeField] private Button btn;
    [SerializeField] private TextMeshProUGUI txtDay;
    [SerializeField] private Image imgToday, imgRedDot, imgPurpleDot;
    /// <summary>
    /// 文字顏色：今天、週末、平日、上下個月
    /// </summary>
    [SerializeField] private Color colorToday, colorWeekend, colorDay, colorWeekendPassed, colorDayPassed;

    /// <summary>
    /// 日期資料
    /// </summary>
    public DateTime dateTime { get; private set; }

    private void Awake() => btn.onClick.AddListener(() => onClick?.Invoke(dateTime));

    /// <summary>
    /// 設定日期
    /// </summary>
    public void SetDateTime(DateTime date)
    {
        dateTime = date;
        txtDay.SetText(date.Day.ToString());

        bool isWeekend = dateTime.DayOfWeek == DayOfWeek.Sunday || dateTime.DayOfWeek == DayOfWeek.Saturday;
        bool isCurrentMonth = dateTime.Year == DateTime.Today.Year && dateTime.Month == DateTime.Today.Month;

        // 依日期設置顏色
        imgToday.gameObject.SetActive(dateTime == DateTime.Today);
        if (imgToday.gameObject.activeSelf) txtDay.color = colorToday;
        else if (isWeekend)
            txtDay.color = (isCurrentMonth) ? colorWeekend : colorWeekendPassed;
        else txtDay.color = (isCurrentMonth) ? colorDay : colorDayPassed;
    }

    private void OnValidate()
    {
        btn ??= GetComponent<Button>();
        txtDay ??= transform.Find("txtDay").GetComponent<TextMeshProUGUI>();
        imgToday ??= transform.Find("imgToday").GetComponent<Image>();
        imgRedDot ??= transform.Find("imgRedDot").GetComponent<Image>();
        imgPurpleDot ??= transform.Find("imgPurpleDot").GetComponent<Image>();
    }
}
