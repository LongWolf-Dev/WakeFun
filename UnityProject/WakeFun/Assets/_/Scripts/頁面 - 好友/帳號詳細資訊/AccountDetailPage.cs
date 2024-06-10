using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountDetailPage : MonoBehaviour
{
    [SerializeField] private SO_Account accountSoData;

    [Header(">>>大頭照")]
    [SerializeField] private Image imgAvatar;
    [Header(">>>法號")]
    [SerializeField] private TextMeshProUGUI txtTitleName;
    [Header(">>>名稱")]
    [SerializeField] private TextMeshProUGUI txtUserName;
    [Header(">>>年齡")]
    [SerializeField] private TextMeshProUGUI txtAge;
    [Header(">>>關於我")]
    [SerializeField] private TextMeshProUGUI txtAboutMe;
    [Header(">>>性別")]
    [SerializeField] private TextMeshProUGUI txtGender;

    [Header(">>>點讚數")]
    [SerializeField] private TextMeshProUGUI txtLiked;
    [Header(">>>醒樂點")]
    [SerializeField] private TextMeshProUGUI txtWakeFunPoint;



    public void SetAccountSoData(SO_Account soData)
    {
        Debug.Log($"SetAccountSoData: {soData}");
        accountSoData = soData;
        gameObject.SetActive(true);
        RefreshData();
    }

    private void OnValidate()
    {
        imgAvatar ??= transform.Find("大頭照").GetChild(1).GetChild(0).GetComponent<Image>();

        Transform panel = transform.Find("Panel面板").transform;
        txtTitleName ??= panel.Find("法號").GetChild(0).GetComponent<TextMeshProUGUI>();
        txtUserName ??= panel.Find("名稱").GetChild(0).GetComponent<TextMeshProUGUI>();
        txtAge ??= panel.Find("年齡").GetChild(0).GetComponent<TextMeshProUGUI>();
        txtAboutMe ??= panel.Find("關於我").GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        txtGender ??= panel.Find("txt性別").GetComponent<TextMeshProUGUI>();
    }

    private void RefreshData()
    {
        if (accountSoData.Avatar != null) imgAvatar.sprite = accountSoData.Avatar;
        txtLiked.text = accountSoData.NumOfLiked.ToString();
        txtTitleName.text = accountSoData.TitleName;
        txtUserName.text = accountSoData.UserName;
        txtAge.text = accountSoData.Age.ToString();
        txtAboutMe.text = accountSoData.AboutMe;
        txtGender.text = accountSoData.Gender.ToString();
    }
}
