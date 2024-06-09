using TMPro;
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

    [Header(">>> UI�ե�")]
    [SerializeField] private Button btnDetail;
    [SerializeField] private Toggle toggleLiked;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtNameTitle;

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
        txtName ??= transform.Find("txt�m�W").GetComponent<TextMeshProUGUI>();
        txtNameTitle ??= transform.Find("txt�k��").GetComponent<TextMeshProUGUI>();
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
            }
        }
        else
        {
            SO_Account tempData = ScriptableObject.CreateInstance<SO_Account>();
            tempData._SetupRandomData();
            txtName.text = tempData.UserName;
            txtNameTitle.text = tempData.TitleName;
        }
    }
}
