using UnityEngine;

/// <summary>
/// ���N���
/// </summary>
[CreateAssetMenu(fileName = "SO_���N���", menuName = ">>WakeFun<</ScriptableObject/SO_���N���")]
public class SO_Achievement : ScriptableObject
{
    [Header(">>> ���N���D")]
    [SerializeField] private string title;
    [Header(">>> ���N���� ")]
    [SerializeField] private string description;
    [Header(">>> �O�_�F��")]
    [SerializeField] private bool isAchieved;
}