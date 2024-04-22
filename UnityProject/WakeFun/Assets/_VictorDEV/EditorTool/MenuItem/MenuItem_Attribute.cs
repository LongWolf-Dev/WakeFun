using UnityEditor;
using UnityEngine;

namespace VictorDev.EditorTool
{
    /// <summary>
    /// MenuItem工具列 擴充功能 - GameObject屬性管理
    /// </summary>
    public class MenuItem_Attribute : MonoBehaviour
    {
        [MenuItem(">>VictorDev<</GameObject屬性管理/所選物件設置為Occluder、Occludee #o")]
        static void SetOccluderOccludee()
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                if (HasDesiredStaticFlags(obj))
                {
                    ClearStaticFlagsRecursively(obj);
                }
                else
                {
                    SetStaticFlagsRecursively(obj);
                }
            }
        }

        static bool HasDesiredStaticFlags(GameObject obj)
        {
            var flags = GameObjectUtility.GetStaticEditorFlags(obj);
            return (flags & (StaticEditorFlags.OccludeeStatic | StaticEditorFlags.OccluderStatic | StaticEditorFlags.BatchingStatic)) != 0;
        }

        static void SetStaticFlagsRecursively(GameObject obj)
        {
            GameObjectUtility.SetStaticEditorFlags(obj,
                StaticEditorFlags.OccludeeStatic |
                StaticEditorFlags.OccluderStatic |
                StaticEditorFlags.BatchingStatic);

            foreach (Transform child in obj.transform)
            {
                SetStaticFlagsRecursively(child.gameObject);
            }
        }

        static void ClearStaticFlagsRecursively(GameObject obj)
        {
            GameObjectUtility.SetStaticEditorFlags(obj,
                GameObjectUtility.GetStaticEditorFlags(obj) & ~(StaticEditorFlags.OccludeeStatic | StaticEditorFlags.OccluderStatic | StaticEditorFlags.BatchingStatic));

            foreach (Transform child in obj.transform)
            {
                ClearStaticFlagsRecursively(child.gameObject);
            }
        }
    }
}
