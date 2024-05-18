using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 頁面主體 - 成就
/// </summary>
public class PageHandler_Achievement : MonoBehaviour
{
    [Header(">>> 返回鈕")]
    [SerializeField] private Button btnReturn;

    [Header(">>> 成就列表")]
    [SerializeField] private AchievementList achievementList;

    private void Awake()
    {
    }

    private void OnValidate()
    {
        btnReturn ??= transform.GetChild(0).Find("Button返回").GetComponent<Button>();
        achievementList ??= transform.Find("成就列表").GetComponent<AchievementList>();
    }
}
