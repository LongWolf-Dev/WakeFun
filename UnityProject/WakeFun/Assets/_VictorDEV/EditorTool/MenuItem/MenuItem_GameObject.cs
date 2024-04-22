using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace VictorDev.EditorTool
{
    /// <summary>
    /// MenuItem工具列 擴充功能 - GameObject管理
    /// </summary>
    public abstract class MenuItem_GameObject : MonoBehaviour
    {

        [MenuItem(">>VictorDev<</GameObject管理/所選物件移動至上一層 %[")]
        static void MoveUpOneLevel()
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                if (obj.transform.parent != null)
                {
                    obj.transform.parent = obj.transform.parent.parent;
                }
            }
        }

        [MenuItem(">>VictorDev<</GameObject管理/選取下一層所有子物件 %]")]
        static void SelectAllChildren()
        {
            // 如果有選擇物件
            if (Selection.gameObjects.Length > 0)
            {
                List<GameObject> selectionAllChild = new List<GameObject>();
                //擷取每個選取物件底下一層的所有子物件
                foreach (GameObject selectionTarget in Selection.gameObjects)
                {
                    foreach (Transform child in selectionTarget.transform)
                    {
                        selectionAllChild.Add(child.gameObject);
                    }
                }
                Selection.objects = selectionAllChild.ToArray();
            }
        }
    }
}
