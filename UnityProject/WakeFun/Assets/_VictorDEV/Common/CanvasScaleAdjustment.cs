using UnityEngine;
using UnityEngine.UI;

public class CanvasScaleAdjustment : MonoBehaviour
{
    [SerializeField] private SizeMatchType sizeMatchType;
    [SerializeField] private CanvasScaler canvasScaler;

    void Start()
    {
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        canvasScaler.matchWidthOrHeight = (int)sizeMatchType;
    }

    private void OnValidate()
    {
        canvasScaler ??= GetComponent<CanvasScaler>();
    }

    private enum SizeMatchType
    {
        Width,  // 0 
        Height  // 1
    }
}
