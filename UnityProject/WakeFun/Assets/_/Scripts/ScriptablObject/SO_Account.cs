using System;
using UnityEngine;
using VictorDev.EditorTool;
using Random = UnityEngine.Random;

/// <summary>
/// �b�����
/// </summary>
[CreateAssetMenu(fileName = "SO_�|���b��", menuName = ">>WakeFun<</ScriptableObject/SO_�|���b��")]
public class SO_Account : ScriptableObject
{
    [Header(">>> �k��")]
    [SerializeField] private string titleName;
    [Header(">>> �m�W")]
    [SerializeField] private string userName;
    [Header(">>> �~��")]
    [SerializeField] private int age;
    [Header(">>> �ʧO")]
    [SerializeField] private EnumGender gender;
    [Header(">>> �����")]
    [TextArea(3, 10)]
    [SerializeField] private string aboutMe;

    [Header(">>> �����I")]
    [Range(0, 99999)]
    [SerializeField] private int wakeFunPoint;
    [Header(">>> �I�g��")]
    [Range(0, 99999)]
    [SerializeField] private int numOfLiked;

    [Header(">>> �j�Y��")]
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
    /// �s�@�üƸ�� (For���ե�)
    /// </summary>
    public void _SetupRandomData()
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