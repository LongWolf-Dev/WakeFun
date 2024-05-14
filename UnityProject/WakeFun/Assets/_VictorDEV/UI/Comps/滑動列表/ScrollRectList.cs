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

    [Header(">>> 垂直滑動列表 - 手動設定scrollRect對像")]
    [SerializeField] protected ScrollRect scrollView;

    /// <summary>
    /// 儲存外部傳來的ScriptateObject資料
    /// </summary>
    public List<SO> soDataList { get; private set; }

    /// <summary>
    /// 以ScriptateObject資料，建置列表資料項T
    /// <para>會先呼叫ToClearList()，以清空原先資料</para>
    /// </summary>
    public void SetDataList(List<SO> datas)
    {
        ToClearList();

        soDataList = datas;

        for (int i = 0; i < soDataList.Count; i++)
        {
            T item = Instantiate(prefabItem, scrollView.content);
            AddActionInSetDataListForLoop(item, soDataList[i]);
        }
    }

    /// <summary>
    /// [abstract] 在SetDataList裡以廻圈建立Item時，順帶觸發的函式
    /// <para>供繼承的子類別實作使用</para>
    /// </summary>
    protected abstract void AddActionInSetDataListForLoop(T item, SO soData);

    /// <summary>
    /// [abstract] 清空列表
    /// </summary>
    protected abstract void ToClearList();

    /// <summary>
    /// 上移至頂部
    /// </summary>
    public void ScrollToTop() => ToScroll(1f);

    /// <summary>
    /// 下移至底部
    /// </summary>
    public void ScrollToBottom() => ToScroll(0f);

    private void ToScroll(float value) => scrollView.verticalNormalizedPosition = value;
}
