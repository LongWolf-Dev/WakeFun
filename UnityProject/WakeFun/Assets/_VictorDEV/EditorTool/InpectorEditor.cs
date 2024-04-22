using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace VictorDev.EditorTool
{
    #region [>>> Editor]
#if UNITY_EDITOR
    /// <summary>
    /// 繼承類別上面需加上
    /// [CustomEditor(typeof(類別名))]
    /// </summary>
    public abstract class InspectorEditor<ClassType> : Editor where ClassType : class, new ()
    {
        /// <summary>
        /// 取得實例
        /// </summary>
        protected ClassType instance => target as ClassType;

        /// <summary>
        /// 新增空格高度
        /// </summary>
        /// <param name="height"></param>
        protected void _CreateSpacer(float height = 10) => EditorGUILayout.Space(height);
        /// <summary>
        /// 新增按鈕
        /// </summary>
        /// <param name="label">按鈕標題</param>
        /// <param name="guiStyle">文字樣式</param>
        /// <param name="action">Click後執行</param>
        /// <param name="height">按鈕高度</param>
        protected void _CreateButton(string label, GUIStyle guiStyle, Action action, bool buttonEnabled = true, int height = 40)
        {
            GUI.enabled = buttonEnabled;
            if (GUILayout.Button(label, guiStyle, GUILayout.Height(height)))
                action?.Invoke();
            GUI.enabled = true;
        }

        /// <summary>
        /// 新增文字標籤
        /// </summary>
        protected void _CreateLabelFiled(string title, GUIStyle textStyle = null)
        {
            textStyle ??= _CreateLabelStyle();
            EditorGUILayout.LabelField(title, textStyle);
        }
        /// <summary>
        /// 新增文字輸入框(單行)
        /// </summary>
        protected void _CreateTextFiled(string title, ref string sourceText) => sourceText = EditorGUILayout.TextField(title, sourceText);
        /// <summary>
        /// 新增文字輸入框(多行)，每一行高約20pixel
        /// </summary>
        protected void _CreateTextArea(ref string content, int numRow = 3, GUIStyle textAreaGUIStyle = null)
        {
            GUILayoutOption layoutOption = GUILayout.Height(25 * numRow);
            content = (textAreaGUIStyle == null) ?
                EditorGUILayout.TextArea(content, layoutOption)
                : EditorGUILayout.TextArea(content, textAreaGUIStyle, layoutOption);
        }
        /// <summary>
        /// 新增ReadMe說明
        /// </summary>
        protected void _CreateReadMe(string context)
        {
            _CreateGUIStyle_TextArea(out GUIStyle gUIStyle, Color.white);
            _SetDisabledGroup(() => _CreateTextArea(ref context, 2, gUIStyle));
        }

        /// <summary>
        /// 建立GUI Style for TextArea
        /// </summary>
        protected void _CreateGUIStyle_TextArea(out GUIStyle gUIStyle, Color textColor, int fontSize = 16)
        {
            gUIStyle = new GUIStyle(EditorStyles.textArea);
            gUIStyle.fontSize = fontSize;
            gUIStyle.normal.textColor = textColor;
        }

        /// <summary>
        /// 設定項目為Disabled
        /// </summary>
        protected void _SetDisabledGroup(Action action)
        {
            EditorGUI.BeginDisabledGroup(true);
            action?.Invoke();
            EditorGUI.EndDisabledGroup();
        }

        /// <summary>
        /// 新增物件欄位(不適用Foreach)
        /// </summary>
        /// <typeparam name="T">組件類別(不適用GameObject)</typeparam>
        /// <param name="title">文字標題</param>
        /// <param name="sourceObject">回傳給原物件</param>
        protected void _CreateObjectFiled<T>(string title, ref T sourceObject) where T : Component
            => sourceObject = EditorGUILayout.ObjectField(title, sourceObject, typeof(T), true) as T;

        /// <summary>
        /// 顯示List泛型列表，需ref儲存收合展開狀態bool
        /// </summary>
        protected void _ShowList<T>(string title, List<T> sourceList, ref bool isShow) where T : Component
        {
            isShow = _CreateFoldout(isShow, $"{title} ({sourceList.Count}筆資料)");

            if (isShow)
            {
                for (int i = 0; i < sourceList.Count; i++)
                {
                    T item = sourceList[i];
                    _CreateObjectFiled($"TargetObject - {i}", ref item);
                }
            }
        }

        /// <summary>
        /// 建立下拉式功能，回傳是否收合展開bool
        /// </summary>
        protected bool _CreateFoldout(bool isShow, string title) => EditorGUILayout.Foldout(isShow, title);

        /// <summary>
        /// 依Enum類別新增下拉式選單
        /// </summary>
        /// <typeparam name="T">Enum類別</typeparam>
        /// <param name="title">文字標題</param>
        /// <param name="sourceEnumValue">回傳Enum值</param>
        protected void _CreateDropDownMenu<T>(string title, ref T sourceEnumValue) where T : Enum
            => sourceEnumValue = (T)EditorGUILayout.EnumPopup(title, sourceEnumValue);


        /// <summary>
        /// 建立GUI Label樣式
        /// </summary>
        protected GUIStyle _CreateLabelStyle(Color textColor = default(Color), int fontSize = 16, FontStyle fontStyle = FontStyle.Normal)
            => _CreateStyle(EditorStyles.label, textColor, fontSize, fontStyle);

        /// <summary>
        /// 建立GUI Button樣式
        /// </summary>
        protected GUIStyle _CreateButtonStyle(Color textColor = default(Color), int fontSize = 16, FontStyle fontStyle = FontStyle.Normal)
            => _CreateStyle(GUI.skin.button, textColor, fontSize, fontStyle);

        /// <summary>
        /// 設定GUI樣式
        /// </summary>
        private GUIStyle _CreateStyle(GUIStyle gUIStyle, Color textColor = default(Color), int fontSize = 16, FontStyle fontStyle = FontStyle.Normal)
        {
            GUIStyle style = new GUIStyle(gUIStyle);
            style.fontSize = fontSize;
            style.normal.textColor = (textColor == default(Color)) ? Color.yellow : textColor;
            style.fontStyle = fontStyle;
            return style;
        }



        /// <summary>
        /// 設定垂直排列版面
        /// </summary>
        protected void _SetVerticalLayout(Action action)
        {
            GUILayout.BeginVertical("Box");
            action?.Invoke();
            GUILayout.EndVertical();
        }
        /// <summary>
        /// 設定水平排列版面
        /// </summary>
        protected void _SetHorizontalLayout(Action action)
        {
            GUILayout.BeginHorizontal("Box");
            action?.Invoke();
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// 設置下拉式區塊
        /// </summary>
        /// <param name="isOpenStatus">是否開啟區塊 (需設為區域變數)</param>
        protected void _SetDropDownFoldout(ref bool isOpenStatus, string title, Action action)
        {
            isOpenStatus = EditorGUILayout.Foldout(isOpenStatus, title);

            // 如果下拉式區塊被展開，顯示相應的選項
            if (isOpenStatus)
            {
                EditorGUI.indentLevel++; // 增加縮排，使其顯示為子選項
                action?.Invoke();
                EditorGUI.indentLevel--; // 恢復縮排
            }
        }
    }
#endif
    #endregion
}
