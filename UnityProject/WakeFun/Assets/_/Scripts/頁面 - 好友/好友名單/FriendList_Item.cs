using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// �C��ե�Item - �n�ͦW��
/// </summary>
[RequireComponent(typeof(Button))]
public class FriendList_Item : MonoBehaviour
{
    [Header(">>> �C��ե�Item - �n�ͦW��")]
    [SerializeField] private SO_Account accountSO;

    [SerializeField] private Button btnDetail;
    [SerializeField] private Toggle toggleLiked;
    [SerializeField] private Text txtName;
    [SerializeField] private Text txtNameTitle;

    /// <summary>
    /// �ƥ�G�I������
    /// </summary>
    public UnityEvent<SO_Account> OnClickDetailButton;
    /// <summary>
    /// �ƥ�G�I��+���s
    /// </summary>
    public UnityEvent<SO_Account, bool> OnClickLikedToggle;

    /// <summary>
    /// �]�mToggle�g��group
    /// </summary>
    public ToggleGroup toggleGroup { set => toggleLiked.group = value; }

    public SO_Account AccountSO
    {
        set
        {
            accountSO = value;
            RefreshData();
        }
    }

    private void Awake()
    {
        btnDetail.onClick.AddListener(() => OnClickDetailButton?.Invoke(accountSO));
        toggleLiked.onValueChanged.AddListener((isOn) => OnClickLikedToggle?.Invoke(accountSO, isOn));
    }

    private void OnValidate()
    {
        btnDetail ??= GetComponent<Button>();
        toggleLiked ??= transform.Find("Toggle�g").GetComponent<Toggle>();
        txtName ??= transform.Find("�m�W").GetComponent<Text>();
        txtNameTitle ??= transform.Find("�k��").GetComponent<Text>();
        RefreshData();
    }

    private void RefreshData()
    {
        if (accountSO != null)
        {
            if (txtName.text != accountSO.UserName)
            {
                txtName.text = accountSO.UserName;
                txtNameTitle.text = accountSO.TitleName;
                name = $"{GetType().Name} - {accountSO.UserName}";
            }
        }
        else
        {
            SO_Account tempData = ScriptableObject.CreateInstance<SO_Account>();
            tempData._SetupRandomData();
            txtName.text = tempData.UserName;
            txtNameTitle.text = tempData.TitleName;
            name = $"{GetType().Name} - {tempData.UserName}";

            Debug.LogWarning($"[Random Data] >>> {name}");
        }
    }
}
