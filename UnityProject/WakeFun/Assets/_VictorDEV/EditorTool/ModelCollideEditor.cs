using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VictorDev.Common;
using VictorDev.ComponentUtils;
using VictorDev.EditorTool;
using VictorDev.EditorTool.ColliderUtils;

/// <summary>
/// 判斷對像的Mesh來新增相對應的Collider類型(邏輯判斷)
/// </summary>
public class ModelCollideEditor : SingletonMonoBehaviour<ModelCollideEditor>
{
    [Header(">>> 目標根物件")]
    [SerializeField] private List<Transform> targetList;

    [Header(">>> 欲排除的物件LayerMask")]
    [SerializeField] private LayerMask excludeLayerMask;

    [Space(20)]
    [Header(">>> 欲新增Collider的物件 (僅供顯示在Inspector，不需要設定)")]
    [SerializeField] private List<MeshRenderer> meshRenderList = new List<MeshRenderer>();

    /// <summary>
    /// 新增Collider至目標物件上
    /// </summary>
    public void AddCollider() => Handler((target) => ColliderHandler.AddCollider(target));
    /// <summary>
    ///  從目標物件移除Collider
    /// </summary>
    public void RemoveCollider() => Handler((target) => ColliderHandler.RemoveCollider(target));

    private void Handler(Action<Transform> action)
    {
        GetMeshTarget();
        foreach (MeshRenderer target in meshRenderList)
        {
            if (LayerMaskHandler.IsSameLayerMask(target.gameObject, excludeLayerMask)) continue;
            action.Invoke(target.transform);
        }
    }
    /// <summary>
    /// 以targetList依序取得每個目標底下所有具備MeshRenderer的物件
    /// </summary>
    private void GetMeshTarget()
    {
        meshRenderList.Clear();

        foreach (Transform target in targetList)
        {
            if (target == null) continue;
            meshRenderList.AddRange(ComponetHandler.GetAllChildOfTargetWithComponent<MeshRenderer>(target));
        }
    }

    [CustomEditor(typeof(ModelCollideEditor))]
    private class Inspector : InspectorEditor<ModelCollideEditor>
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUIStyle btnStyle = _CreateButtonStyle();
            _SetHorizontalLayout(() =>
            {
                _CreateButton("設定Colllider", btnStyle, instance.AddCollider);
                _CreateButton("移除Colllider", btnStyle, instance.RemoveCollider);
            });
        }
    }
}
