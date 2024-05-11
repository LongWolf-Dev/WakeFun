using UnityEngine;
using UnityEngine.UI;

namespace VictorDev.UI.Comps
{
    /// <summary>
    /// 視窗組件 - 純線框視窗
    /// </summary>
    public class OutlinePanel : UIPanel
    {
        private void OnValidate()
        {
            txtTitle ??= transform.Find("Text_Title").GetComponent<Text>();
            btnScale ??= GetComponent<Button>();
            btnClose ??= transform.Find("Bkg").GetChild(1).Find("CloseButton").GetComponent<Button>();
            container ??= transform.Find("Bkg").Find("Container");

            txtTitle.text = titleText;
        }
    }
}
