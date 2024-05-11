using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AddFriendHandler : MonoBehaviour
{

    [Header(">>> 搜尋到的用戶資料")]
    [SerializeField] private SO_Account accountSoDataBySearch;

    [Header(">>> 找不到此好友")]
    [SerializeField] private GameObject txtCantFindAccount;

    [Header(">>> 顯示好友名稱")]
    [SerializeField] private Text txtFriendName;

    [Header(">>> 文字輸入框：搜尋ID")]
    [SerializeField] private InputField inputFieldSearchID;
    [Header(">>> 新增友按鈕")]
    [SerializeField] private Button btnAddFriend;
    [Header(">>> 搜尋按鈕")]
    [SerializeField] private Button btnSearch;

    [Header(">>> 好友大頭照")]
    [SerializeField] private Image imgAvatar;

    public SO_Account AccountSoDataBySearch
    {
        get => accountSoDataBySearch;
        set
        {
            accountSoDataBySearch = value;
            txtCantFindAccount.SetActive(accountSoDataBySearch == null);
            RefreshData();
        }
    }

    [Header(">>> 當點擊搜尋按鈕時")]
    public UnityEvent<string, AddFriendHandler> OnSearchIDEvent = new UnityEvent<string, AddFriendHandler>();
    [Header(">>> 當點擊新增好友按鈕時")]
    public UnityEvent<SO_Account> OnAddFriendEvent = new UnityEvent<SO_Account>();

    private void Awake()
    {
        txtCantFindAccount.SetActive(false);
        btnSearch.onClick.AddListener(ToSearchAccount);
        btnAddFriend.onClick.AddListener(() => OnAddFriendEvent.Invoke(accountSoDataBySearch));
    }

    public void ToSearchAccount() => OnSearchIDEvent.Invoke(inputFieldSearchID.text, this);

    private void Update()
    {
        btnAddFriend.gameObject.SetActive(accountSoDataBySearch != null);
        btnSearch.interactable = string.IsNullOrEmpty(inputFieldSearchID.text) == false;
    }

    private void RefreshData()
    {
        if (accountSoDataBySearch == null) return;
        txtFriendName.text = accountSoDataBySearch.UserName;
        imgAvatar.sprite = accountSoDataBySearch.Avatar;
    }



    private void OnValidate()
    {
        Transform panel = transform.GetChild(1);
        txtCantFindAccount ??= panel.Find("Text找不到").gameObject;
        txtFriendName ??= panel.Find("txt好友名稱").GetComponent<Text>();
        imgAvatar ??= panel.Find("大頭照外框").GetChild(0).GetChild(0).GetComponent<Image>();
        inputFieldSearchID ??= panel.Find("InputField_好友名稱").GetComponent<InputField>();
        btnAddFriend ??= panel.Find("Button新增").GetComponent<Button>();
        btnSearch ??= panel.Find("Button搜尋").GetComponent<Button>();
    }
}
