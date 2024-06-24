using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalendarController : MonoBehaviour
{
    [Header(">>> 日期Prefab")]
    [SerializeField] private Calendar_DayItem dayItemPrefab;
    [Header(">>> 年週期Prefab")]
    [SerializeField] private TextMeshProUGUI weekOfYearItemPrefab;

    [Header(">>> UI組件")]
    [SerializeField] private Button btnMonthAndYear;
    [SerializeField] private Button btnLastMonth, btnNextMonth;
    [SerializeField] private TextMeshProUGUI txtMonthAndYear;
    [SerializeField] private RectTransform gridDayRect, weekOfYearRect;

    private DateTime currentDate;

    private void Awake()
    {
        btnMonthAndYear.onClick.AddListener(ShowCurrentMonth);
        btnLastMonth.onClick.AddListener(OnPreviousMonth);
        btnNextMonth.onClick.AddListener(OnNextMonth);
    }

    private void Start() => ShowCurrentMonth();
    private void ShowCurrentMonth()
    {
        currentDate = DateTime.Now;
        CreateCalendar(currentDate);
    }

    /// <summary>
    /// 建立行事曆 日期物件
    /// </summary>
    private void CreateCalendar(DateTime date)
    {
        txtMonthAndYear.text = date.ToString("yyyy年 M月");
        // 反向循環刪除所有子元素
        for (int i = gridDayRect.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(gridDayRect.GetChild(i).gameObject);
        }

        //當月有幾天
        int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
        //1號是週幾
        DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
        int startDay = (int)firstDayOfMonth.DayOfWeek;

        CreateWeekOfYear(firstDayOfMonth);

        // 顯示上個月的日期
        DateTime prevMonth = date.AddMonths(-1);
        int daysInPrevMonth = DateTime.DaysInMonth(prevMonth.Year, prevMonth.Month);
        for (int i = startDay - 1; i >= 0; i--)
        {
            Calendar_DayItem dayButton = Instantiate(dayItemPrefab, gridDayRect);
            dayButton.SetDateTime(new DateTime(prevMonth.Year, prevMonth.Month, daysInPrevMonth - i));
        }

        // 這個月
        for (int day = 1; day <= daysInMonth; day++)
        {
            Calendar_DayItem dayButton = Instantiate(dayItemPrefab, gridDayRect);
            dayButton.SetDateTime(new DateTime(date.Year, date.Month, day));
        }

        // 顯示下個月的日期
        DateTime nextMonth = date.AddMonths(1);
        int remainingDays = (6 * 7) - gridDayRect.childCount;

        Debug.Log($"{remainingDays} / {gridDayRect.childCount}");

        for (int i = 1; i <= remainingDays; i++)
        {
            Calendar_DayItem dayButton = Instantiate(dayItemPrefab, gridDayRect);
            dayButton.SetDateTime(new DateTime(nextMonth.Year, nextMonth.Month, i));
        }
    }

    /// <summary>
    /// 建立行事曆 週期
    /// </summary>
    private void CreateWeekOfYear(DateTime dateTime)
    {
        // 反向循環刪除所有子元素
        for (int i = weekOfYearRect.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(weekOfYearRect.GetChild(i).gameObject);
        }

        CultureInfo currentCulture = CultureInfo.CurrentCulture;

        // 获取当前文化的日历
        Calendar calendar = currentCulture.Calendar;

        // 指定周的规则和第一天
        CalendarWeekRule weekRule = currentCulture.DateTimeFormat.CalendarWeekRule;
        DayOfWeek firstDayOfWeek = currentCulture.DateTimeFormat.FirstDayOfWeek;

        // 计算今天是今年的第几周
        int weekOfYear = calendar.GetWeekOfYear(dateTime, weekRule, firstDayOfWeek);
        Debug.Log($"weekOfYear: {weekOfYear}");

        for (int i = 0; i < 6; i++)
        {
            TextMeshProUGUI item = Instantiate(weekOfYearItemPrefab, weekOfYearRect);
            item.SetText(((weekOfYear + i) % 53 + 1).ToString());
        }
    }


    /// <summary>
    /// 下一個月
    /// </summary>
    public void OnNextMonth()
    {
        currentDate = currentDate.AddMonths(1);
        CreateCalendar(currentDate);
    }

    /// <summary>
    /// 前一個月
    /// </summary>
    public void OnPreviousMonth()
    {
        currentDate = currentDate.AddMonths(-1);
        CreateCalendar(currentDate);
    }


    private void OnValidate()
    {
        btnMonthAndYear ??= transform.Find("Header").Find("btnMonthAndYear").GetComponent<Button>();
        btnLastMonth ??= transform.Find("Header").Find("btnLastMonth").GetComponent<Button>();
        btnNextMonth ??= transform.Find("Header").Find("btnNextMonth").GetComponent<Button>();
        txtMonthAndYear ??= btnMonthAndYear.transform.Find("txtMonthAndYear").GetComponent<TextMeshProUGUI>();
        gridDayRect ??= transform.Find("GridDay").GetComponent<RectTransform>();
        weekOfYearRect ??= transform.Find("WeekOfYear").GetComponent<RectTransform>();
    }
}
