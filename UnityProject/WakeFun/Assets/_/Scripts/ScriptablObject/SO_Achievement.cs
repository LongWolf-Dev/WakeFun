using UnityEngine;

/// <summary>
/// 成就資料
/// </summary>
[CreateAssetMenu(fileName = "SO_成就資料", menuName = ">>WakeFun<</ScriptableObject/SO_成就資料")]
public class SO_Achievement : ScriptableObject
{
    [Header(">>> 成就標題")]
    [SerializeField] private string title;
    [Header(">>> 成就說明 ")]
    [SerializeField] private string description;
    [Header(">>> 是否達成")]
    [SerializeField] private bool isAchieved;
}