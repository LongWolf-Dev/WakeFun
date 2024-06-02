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

    [Header(">>> UI組件 - 顯示頁面")]
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
    [SerializeField] private TextMeshProUGUI inputAboutMe;
    [SerializeField] private Button btnSend;

    public SO_Account AccountData
    {
        get => soAccountData;
        set
        {
            soAccountData = value;
            editAccountData = value;
        }
    }

    private void Awake()
    {
        btnSend.onClick.AddListener(OnClickSendButton);
    }

    private void OnClickSendButton()
    {
        OnFinishEdit?.Invoke(editAccountData);
    }
}
