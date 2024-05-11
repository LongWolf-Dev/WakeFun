using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VictorDev.Common
{
    /// <summary>
    /// 3D物件點擊組件
    /// <para> + 需有Collide組件才可以用滑鼠互動</para>
    /// <para> + OnMouseEnterEvent : 滑鼠移入</para>
    /// <para> + OnMouseDownEvent : 滑鼠左鍵按下</para>
    /// <para> + OnMouseUpEvent : 滑鼠左鍵放開</para>
    /// <para> + OnMouseExitEvent : 滑鼠移出</para>
    /// <para> + OnMouseClickEvent : 滑鼠點擊</para>
    /// </summary>
    public class ClickableObject : MonoBehaviour
    {
        public bool isClickable { get; set; } = true;
        public bool isSelected { get; set; } = false;

        public event Action<ClickableObject> OnMouseEnterEvent, OnMouseDownEvent, OnMouseUpEvent, OnMouseExitEvent, OnMouseClickEvent;
        public GameObject parentObject { get; set; }

        private bool isMouseDown = false;

        private void OnMouseEnter() => CheckEvent(OnMouseEnterEvent);
        private void OnMouseDown()
        {
            isMouseDown = true;
            CheckEvent(OnMouseDownEvent);
        }
        private void OnMouseUp()
        {
            isSelected = true;
            if (isMouseDown)
            {
                CheckEvent(OnMouseClickEvent);
            }
            isMouseDown = false;
            CheckEvent(OnMouseUpEvent);
        }
        private void OnMouseExit()
        {
            isMouseDown = false;
            CheckEvent(OnMouseExitEvent);
        }

        private void CheckEvent(Action<ClickableObject> callback)
        {
            if (isClickable)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    callback?.Invoke(this);
                }
            }
        }
    }
}
