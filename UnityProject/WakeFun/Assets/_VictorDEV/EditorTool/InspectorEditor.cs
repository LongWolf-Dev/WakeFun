using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace VictorDev.EditorTool
{
    #region [>>> Editor]
#if UNITY_EDITOR
    /// <summary>
    /// �~�����O�W���ݥ[�W
    /// [CustomEditor(typeof(���O�W))]
    /// </summary>
    public abstract class InspectorEditor<ClassType> : Editor where ClassType : class, new()
    {
        /// <summary>
        /// ���o���
        /// </summary>
        protected ClassType instance => target as ClassType;

        /// <summary>
        /// �s�W�Ů氪��
        /// </summary>
        /// <param name="height"></param>
        protected void _CreateSpacer(float height = 10) => EditorGUILayout.Space(height);
        /// <summary>
        /// �s�W���s
        /// </summary>
        /// <param name="label">���s���D</param>
        /// <param name="guiStyle">��r�˦�</param>
        /// <param name="action">Click�����</param>
        /// <param name="height">���s����</param>
        protected void _CreateButton(string label, GUIStyle guiStyle, Action action, bool buttonEnabled = true, int height = 40)
        {
            GUI.enabled = buttonEnabled;
            if (GUILayout.Button(label, guiStyle, GUILayout.Height(height)))
                action?.Invoke();
            GUI.enabled = true;
        }

        /// <summary>
        /// �s�W��r����
        /// </summary>
        protected void _CreateLabelFiled(string title, GUIStyle textStyle = null)
        {
            textStyle ??= _CreateLabelStyle();
            EditorGUILayout.LabelField(title, textStyle);
        }
        /// <summary>
        /// �s�W��r��J��(���)
        /// </summary>
        protected void _CreateTextFiled(string title, ref string sourceText) => sourceText = EditorGUILayout.TextField(title, sourceText);
        /// <summary>
        /// �s�W��r��J��(�h��)�A�C�@�氪��20pixel
        /// </summary>
        protected void _CreateTextArea(ref string content, int numRow = 3, GUIStyle textAreaGUIStyle = null)
        {
            GUILayoutOption layoutOption = GUILayout.Height(25 * numRow);
            content = (textAreaGUIStyle == null) ?
                EditorGUILayout.TextArea(content, layoutOption)
                : EditorGUILayout.TextArea(content, textAreaGUIStyle, layoutOption);
        }
        /// <summary>
        /// �s�WReadMe����
        /// </summary>
        protected void _CreateReadMe(string context)
        {
            _CreateGUIStyle_TextArea(out GUIStyle gUIStyle, Color.white);
            _SetDisabledGroup(() => _CreateTextArea(ref context, 2, gUIStyle));
        }

        /// <summary>
        /// �إ�GUI Style for TextArea
        /// </summary>
        protected void _CreateGUIStyle_TextArea(out GUIStyle gUIStyle, Color textColor, int fontSize = 16)
        {
            gUIStyle = new GUIStyle(EditorStyles.textArea);
            gUIStyle.fontSize = fontSize;
            gUIStyle.normal.textColor = textColor;
        }

        /// <summary>
        /// �]�w���ج�Disabled
        /// </summary>
        protected void _SetDisabledGroup(Action action)
        {
            EditorGUI.BeginDisabledGroup(true);
            action?.Invoke();
            EditorGUI.EndDisabledGroup();
        }

        /// <summary>
        /// �s�W�������(���A��Foreach)
        /// </summary>
        /// <typeparam name="T">�ե����O(���A��GameObject)</typeparam>
        /// <param name="title">��r���D</param>
        /// <param name="sourceObject">�^�ǵ��쪫��</param>
        protected void _CreateObjectFiled<T>(string title, ref T sourceObject) where T : Component
            => sourceObject = EditorGUILayout.ObjectField(title, sourceObject, typeof(T), true) as T;

        /// <summary>
        /// ���List�x���C��A��ref�x�s���X�i�}���Abool
        /// </summary>
        protected void _ShowList<T>(string title, List<T> sourceList, ref bool isShow) where T : Component
        {
            isShow = _CreateFoldout(isShow, $"{title} ({sourceList.Count}�����)");

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
        /// �إߤU�Ԧ��\��A�^�ǬO�_���X�i�}bool
        /// </summary>
        protected bool _CreateFoldout(bool isShow, string title) => EditorGUILayout.Foldout(isShow, title);

        /// <summary>
        /// ��Enum���O�s�W�U�Ԧ����
        /// </summary>
        /// <typeparam name="T">Enum���O</typeparam>
        /// <param name="title">��r���D</param>
        /// <param name="sourceEnumValue">�^��Enum��</param>
        protected void _CreateDropDownMenu<T>(string title, ref T sourceEnumValue) where T : Enum
            => sourceEnumValue = (T)EditorGUILayout.EnumPopup(title, sourceEnumValue);


        /// <summary>
        /// �إ�GUI Label�˦�
        /// </summary>
        protected GUIStyle _CreateLabelStyle(Color textColor = default(Color), int fontSize = 16, FontStyle fontStyle = FontStyle.Normal)
            => _CreateStyle(EditorStyles.label, textColor, fontSize, fontStyle);

        /// <summary>
        /// �إ�GUI Button�˦�
        /// </summary>
        protected GUIStyle _CreateButtonStyle(Color textColor = default(Color), int fontSize = 16, FontStyle fontStyle = FontStyle.Normal)
            => _CreateStyle(GUI.skin.button, textColor, fontSize, fontStyle);

        /// <summary>
        /// �]�wGUI�˦�
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
        /// �]�w�����ƦC����
        /// </summary>
        protected void _SetVerticalLayout(Action action)
        {
            GUILayout.BeginVertical("Box");
            action?.Invoke();
            GUILayout.EndVertical();
        }
        /// <summary>
        /// �]�w�����ƦC����
        /// </summary>
        protected void _SetHorizontalLayout(Action action)
        {
            GUILayout.BeginHorizontal("Box");
            action?.Invoke();
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// �]�m�U�Ԧ��϶�
        /// </summary>
        /// <param name="isOpenStatus">�O�_�}�Ұ϶� (�ݳ]���ϰ��ܼ�)</param>
        protected void _SetDropDownFoldout(ref bool isOpenStatus, string title, Action action)
        {
            isOpenStatus = EditorGUILayout.Foldout(isOpenStatus, title);

            // �p�G�U�Ԧ��϶��Q�i�}�A��ܬ������ﶵ
            if (isOpenStatus)
            {
                EditorGUI.indentLevel++; // �W�[�Y�ơA�Ϩ���ܬ��l�ﶵ
                action?.Invoke();
                EditorGUI.indentLevel--; // ��_�Y��
            }
        }

        /// <summary>
        /// �e�@�������u
        /// </summary>
        protected void _DrawHorizontalLine(Color color = default)
        {
            if (color == default(Color)) color = Color.gray;

            Rect rect = EditorGUILayout.GetControlRect(false, 1);
            EditorGUI.DrawRect(rect, color);
            rect.height = 1;
            rect.y += 1;
            EditorGUI.DrawRect(rect, color);
        }
    }
#endif
    #endregion
}