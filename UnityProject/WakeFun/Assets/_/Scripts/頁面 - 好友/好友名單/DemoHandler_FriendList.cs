using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Demo產生資料用
/// </summary>
public class DemoHandler_FriendList : MonoBehaviour
{
    [SerializeField] private int numOfData = 15;
    [SerializeField] private FriendList targetList;

    private void Start() => CreateData();

    [ContextMenu("- Create Data")]
    private void CreateData()
    {
        List<SO_Account> resultList = new List<SO_Account>();

        for (int i = 0; i < numOfData; i++)
        {
            SO_Account item = ScriptableObject.CreateInstance<SO_Account>();
            item._SetupRandomData();
            resultList.Add(item);
        }
        targetList.SetDataList(resultList);
    }

    private void OnValidate()
    {
        name = "_DemoHandler";
        targetList ??= transform.parent.GetComponent<FriendList>();
    }
}
