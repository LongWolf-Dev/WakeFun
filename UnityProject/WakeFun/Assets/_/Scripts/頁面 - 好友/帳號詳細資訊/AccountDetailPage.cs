using UnityEngine;
using UnityEngine.UI;

public class AccountDetailPage : MonoBehaviour
{
    [SerializeField] private SO_Account accountSoData;

    [Header(">>>大頭照")]
    [SerializeField] private Image imgAvatar;
    [Header(">>>法號")]
    [SerializeField] private Text txtTitleName;
    [Header(">>>名稱")]
    [SerializeField] private Text txtUserName;
    [Header(">>>年齡")]
    [SerializeField] private Text txtAge;
    [Header(">>>關於我")]
    [SerializeField] private Text txtAboutMe;
    [Header(">>>性別")]
    [SerializeField] private Text txtGender;

    [Header(">>>點讚數")]
    [SerializeField] private Text txtLiked;
    [Header(">>>醒樂點")]
    [SerializeField] private Text txtWakeFunPoint;



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
        txtTitleName ??= panel.Find("法號").GetChild(0).GetComponent<Text>();
        txtUserName ??= panel.Find("名稱").GetChild(0).GetComponent<Text>();
        txtAge ??= panel.Find("年齡").GetChild(0).GetComponent<Text>();
        txtAboutMe ??= panel.Find("關於我").GetChild(0).GetChild(0).GetComponent<Text>();
        txtGender ??= panel.Find("txt性別").GetComponent<Text>();
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
