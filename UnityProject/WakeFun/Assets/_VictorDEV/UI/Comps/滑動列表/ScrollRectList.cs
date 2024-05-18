using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// [abstract] 垂直滑動列表
/// <para> T：列表Item的Component組件</para>
/// <para> SO：列表Item會用到的ScriptableObject資料</para>
/// </summary>
public abstract class ScrollRectList<T, SO> : MonoBehaviour where T : Component where SO : ScriptableObject
{
    [Header(">>> 列表項目組件(Prefab)")]
    [SerializeField] private T prefabItem;

    [Header(">>> 列表容器")]
    [SerializeField] private ScrollRect scrollRect;

    [Header(">>> 顯示列表項目總數量")]
    [SerializeField] private Text txtAmountOfDatas;

    [Header(">>> 當列表項目被點擊時發送事件")]
    public UnityEvent<SO> onItemClicked;

    /// <summary>
    /// 儲存外部傳來的ScriptateObject資料
    /// </summary>
    public List<SO> soDataList { get; private set; }

    /// <summary>
    /// 以ScriptateObject資料，建置列表資料項T
    /// </summary>
    public void SetDataList(List<SO> datas)
    {
        ToClearData();

        soDataList = datas;
        if (txtAmountOfDatas != null) txtAmountOfDatas.text = soDataList.Count.ToString();

        for (int i = 0; i < soDataList.Count; i++)
        {
            T item = ObjectPoolManager.GetInstanceFromQueuePool<T>(prefabItem);
            item.transform.SetParent(scrollRect.content);
            AddActionInSetDataListForLoop(item, soDataList[i]);
        }
    }

    /// <summary>
    /// [abstract] 在SetDataList裡以廻圈建立Item時，順帶觸發的函式
    /// <para>供繼承的子類別實作使用</para>
    /// </summary>
    protected abstract void AddActionInSetDataListForLoop(T item, SO soData);

    public void DestroyAllChildren(Transform target)
    {
        foreach (Transform child in target)
        {
#if UNITY_EDITOR
            DestroyImmediate(child);
#else
            Destroy(child);
#endif
        }
    }

    /// <summary>
    /// 上移至頂部
    /// </summary>
    public void ScrollToTop() => scrollRect.verticalNormalizedPosition = 1f;

    /// <summary>
    /// 下移至底部
    /// </summary>
    public void ScrollToBottom() => scrollRect.verticalNormalizedPosition = 0f;

    /// <summary>
    /// 清空資料
    /// </summary>
    public void ToClearData()
    {
        //移除原列表項目至物件池
        ObjectPoolManager.PushToPool<T>(scrollRect.content);
        OnClearData();
    }
    protected virtual void OnClearData() { }

    private void OnValidate()
    {
        OnValidateAfter();
    }

    protected virtual void OnValidateAfter() { }
}