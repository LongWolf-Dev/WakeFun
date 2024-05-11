using UnityEngine;
using VictorDev.EditorTool;

/// <summary>
/// 帳號資料
/// </summary>
[CreateAssetMenu(fileName = "SO_會員帳號", menuName = ">>WakeFun<</ScriptableObject/SO_會員帳號")]
public class SO_Account1 : ScriptableObject
{
    [Header(">>> 法號")]
    [SerializeField] private string titleName;
    [Header(">>> 姓名")]
    [SerializeField] private string userName;
    [Header(">>> 年齡")]
    [SerializeField] private int age;
    [Header(">>> 性別")]
    [SerializeField] private EnumGender gender;
    [Header(">>> 關於我")]
    [TextArea(3, 10)]
    [SerializeField] private string aboutMe;

    [Header(">>> 醒樂點")]
    [Range(0, 99999)]
    [SerializeField] private int wakeFunPoint;
    [Header(">>> 點讚數")]
    [Range(0, 99999)]
    [SerializeField] private int numOfLiked;

    /// <summary>
    /// 建構式
    /// </summary>
    /// <param name="createRandomData">是否自動產生隨機資料</param>
    public SO_Account1(bool createRandomData = true)
    {
        Debug.Log($"createRandomData: {createRandomData}");
        if (createRandomData) SetupRandomData();
    }
    #region [>>> Getter]
    public string NameTitle => titleName;
    public string UserName => userName;
    public int Age => age;
    public EnumGender Gender => gender;
    public string AboutMe => aboutMe;
    public int WakeFunPoint => wakeFunPoint;
    public int NumOfLiked => numOfLiked;
    #endregion
    public SO_Account1(string titleName, string userName, int age, EnumGender gender, string aboutMe, int wakeFunPoint, int numOfLike)
    {
        this.titleName = titleName;
        this.userName = userName;
        this.age = age;
        this.gender = gender;
        this.aboutMe = aboutMe;
        this.wakeFunPoint = wakeFunPoint;
        this.numOfLiked = numOfLike;
    }

    private void OnValidate() => SetupRandomData();
    public void SetupRandomData()
    {
        if (string.IsNullOrEmpty(userName))
        {
            userName = $"{RandomDataUtils.GetNameByRandom()}";
            gender = (Random.Range(0, 2) == 0 ? EnumGender.male : EnumGender.female);
        }
        titleName = $"{userName.Substring(1, 1)}{userName.Substring(1, 1)}";
        if (age == 0) age = Random.Range(26, 90);
        if (wakeFunPoint == 0) wakeFunPoint = Random.Range(0, 10000);
        if (numOfLiked == 0) numOfLiked = Random.Range(0, 10000);
        if (string.IsNullOrEmpty(aboutMe)) aboutMe = RandomDataUtils.GetLoremContextByRandom();
    }
}