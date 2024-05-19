using System;
using UnityEngine;
using VictorDev.EditorTool;
using Random = UnityEngine.Random;

/// <summary>
/// 帳號資料
/// </summary>
[CreateAssetMenu(fileName = "SO_會員帳號", menuName = ">>WakeFun<</ScriptableObject/SO_會員帳號")]
public class SO_Account : ScriptableObject
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

    [Header(">>> 大頭照")]
    [SerializeField] private Sprite avatar;

    #region [>>> Getter]
    public string TitleName => titleName;
    public string UserName => userName;
    public int Age => age;
    public EnumGender Gender => gender;
    public string AboutMe => aboutMe;
    public int WakeFunPoint => wakeFunPoint;
    public int NumOfLiked => numOfLiked;
    public Sprite Avatar => avatar;
    #endregion
    public SO_Account(string titleName, string userName, int age, EnumGender gender, string aboutMe, int wakeFunPoint, int numOfLike)
    {
        this.titleName = titleName;
        this.userName = userName;
        this.age = age;
        this.gender = gender;
        this.aboutMe = aboutMe;
        this.wakeFunPoint = wakeFunPoint;
        this.numOfLiked = numOfLike;
    }
    /// <summary>
    /// 製作亂數資料 (For測試用)
    /// </summary>
    public void _SetupRandomData()
    {
        if (string.IsNullOrEmpty(userName))
        {
            userName = $"{RandomDataUtils.GetNameByRandom()}";
            gender = (Random.Range(0, 2) == 0 ? EnumGender.男士 : EnumGender.女士);
        }
        titleName = $"{userName.Substring(1, 1)}{userName.Substring(1, 1)}";
        if (age == 0) age = Random.Range(26, 90);
        if (wakeFunPoint == 0) wakeFunPoint = Random.Range(0, 10000);
        if (numOfLiked == 0) numOfLiked = Random.Range(0, 10000);
        if (string.IsNullOrEmpty(aboutMe)) aboutMe = RandomDataUtils.GetLoremContextByRandom();
    }

}