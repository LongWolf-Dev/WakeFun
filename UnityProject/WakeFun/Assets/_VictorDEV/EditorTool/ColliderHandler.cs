using System;
using UnityEngine;
using VictorDev.ComponentUtils;
using Debug = VictorDev.Common.DebugHandler;

namespace VictorDev.EditorTool.ColliderUtils
{
    public abstract class ColliderHandler
    {
        /// <summary>
        /// 新增Collider至目標物件上
        /// <para> + 判斷對像的Mesh來新增相對應的Collider類型</para>
        /// </summary>
        public static void AddCollider(Transform target)
        {
            if (target.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
                AddCollider(meshRenderer);
        }
        /// <summary>
        /// 新增Collider至目標物件上
        /// <para> + 判斷對像的Mesh來新增相對應的Collider類型</para>
        /// </summary>
        public static void AddCollider(MeshRenderer target)
        {
            if (IsHaveAnyCollider(target.transform)) return;
            if (target.TryGetComponent<MeshFilter>(out MeshFilter meshFilter))
            {
                Type type = IsBoxMesh(meshFilter) ? typeof(BoxCollider) : typeof(MeshCollider);
                ComponetHandler.AddComponentToTarget(target.transform, type);
            }
        }

        /// <summary>
        /// 從目標物件移除Collider
        /// </summary>
        public static void RemoveCollider(Transform target)
        {
            ComponetHandler.RemoveComponentFromTarget(target, typeof(BoxCollider));
            ComponetHandler.RemoveComponentFromTarget(target, typeof(MeshCollider));
        }

        /// <summary>
        /// 判斷是否為立方體
        /// </summary>
        public static bool IsBoxMesh(MeshFilter meshFilter) => meshFilter.sharedMesh.vertices.Length == 24;

        /// <summary>
        /// 是否含有任何Collider
        /// </summary>
        public static bool IsHaveAnyCollider(Transform target)
        {
            return target.TryGetComponent<MeshCollider>(out MeshCollider meshCollider)
                || target.TryGetComponent<BoxCollider>(out BoxCollider boxCollider)
                || target.TryGetComponent<SphereCollider>(out SphereCollider sphereCollider)
                || target.TryGetComponent<CapsuleCollider>(out CapsuleCollider capsuleCollider)
                ;
        }
    }
}
