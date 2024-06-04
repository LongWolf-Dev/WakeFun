using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Page_PersonalInfoEditor : MonoBehaviour
{
    [Header(">>> 個人資料SO")]
    [SerializeField] private SO_Account soAccountData;

    [Header(">>> 事件：點擊完成編輯")]
    public UnityEvent<SO_Account> OnFinishEdit;
    private SO_Account editAccountData { get; set; }

    #region [>>> UI組件設定]
    [Header(">>> UI組件 - 顯示頁面")]
    [SerializeField] private Image imgAvatar;
    [SerializeField] private Button btnLineShare;
    [SerializeField] private Button btnLogout;
    [SerializeField] private TextMeshProUGUI txtWakefunPoint;
    [SerializeField] private TextMeshProUGUI txtTitleName, txtUserName, txtAge;
    [SerializeField] private TextMeshProUGUI txtGender;
    [SerializeField] private TextMeshProUGUI txtAboutMe;

    [Header(">>> UI組件 - 修改頁面")]
    [SerializeField] private TMP_InputField inputTitleName;
    [SerializeField] private TMP_InputField inputUserName, inputAge;
    [SerializeField] private TMP_Dropdown dpGender;
    [SerializeField] private TMP_InputField inputAboutMe;
    [SerializeField] private Button btnSend;
    [SerializeField] private Button btnChangeAvatar;
    #endregion

    public SO_Account AccountData
    {
        get => soAccountData;
        set
        {
            soAccountData = value;
            editAccountData = value;
            RefreshUI();
        }
    }

    private void Awake()
    {
        btnSend.onClick.AddListener(OnClickSendButton);
    }

    private void OnClickSendButton()
    {
        EnumGender gender = (dpGender.value == 0) ? EnumGender.女士 : EnumGender.男士;
        editAccountData = new SO_Account(inputTitleName.text, inputUserName.text, int.Parse(inputAge.text), gender, inputAboutMe.text, soAccountData.WakeFunPoint, soAccountData.NumOfLiked);

        OnFinishEdit?.Invoke(editAccountData);
    }

    /// <summary>
    /// 更新介面文字內容
    /// </summary>
    private void RefreshUI()
    {
        txtTitleName.text = inputTitleName.text = soAccountData.TitleName;
        txtUserName.text = inputUserName.text = soAccountData.UserName;
        txtAge.text = inputAge.text = soAccountData.Age.ToString();
        txtGender.text = soAccountData.Gender.ToString();
        dpGender.value = (soAccountData.Gender == EnumGender.女士) ? 0 : 1;
    }
}
