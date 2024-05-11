using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AddFriendHandler : MonoBehaviour
{

    [Header(">>> �j�M�쪺�Τ���")]
    [SerializeField] private SO_Account accountSoDataBySearch;

    [Header(">>> �䤣�즹�n��")]
    [SerializeField] private GameObject txtCantFindAccount;

    [Header(">>> ��ܦn�ͦW��")]
    [SerializeField] private Text txtFriendName;

    [Header(">>> ��r��J�ءG�j�MID")]
    [SerializeField] private InputField inputFieldSearchID;
    [Header(">>> �s�W�ͫ��s")]
    [SerializeField] private Button btnAddFriend;
    [Header(">>> �j�M���s")]
    [SerializeField] private Button btnSearch;

    [Header(">>> �n�ͤj�Y��")]
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

    [Header(">>> ���I���j�M���s��")]
    public UnityEvent<string, AddFriendHandler> OnSearchIDEvent = new UnityEvent<string, AddFriendHandler>();
    [Header(">>> ���I���s�W�n�ͫ��s��")]
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
        txtCantFindAccount ??= panel.Find("Text�䤣��").gameObject;
        txtFriendName ??= panel.Find("txt�n�ͦW��").GetComponent<Text>();
        imgAvatar ??= panel.Find("�j�Y�ӥ~��").GetChild(0).GetChild(0).GetComponent<Image>();
        inputFieldSearchID ??= panel.Find("InputField_�n�ͦW��").GetComponent<InputField>();
        btnAddFriend ??= panel.Find("Button�s�W").GetComponent<Button>();
        btnSearch ??= panel.Find("Button�j�M").GetComponent<Button>();
    }
}
