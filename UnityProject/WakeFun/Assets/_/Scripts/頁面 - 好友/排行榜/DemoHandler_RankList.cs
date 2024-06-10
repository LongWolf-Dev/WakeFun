using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Demo產生資料用
/// </summary>
public class DemoHandler_RankList : MonoBehaviour
{
    [SerializeField] private int numOfData = 15;
    [SerializeField] private RankingList rankList;

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
        rankList.SetDataList(resultList);
    }

    private void OnValidate()
    {
        name = "_DemoHandler";
        rankList ??= transform.parent.GetComponent<RankingList>();
    }
}
