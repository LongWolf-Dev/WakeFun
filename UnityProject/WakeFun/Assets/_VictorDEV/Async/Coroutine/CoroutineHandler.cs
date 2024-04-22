using System.Collections;
using VictorDev.Common;

/// <summary>
/// �����OMonoBeehaviour�����O�i��static�I�s�ϥ�
/// </summary>
public class CoroutineHandler : SingletonMonoBehaviour<CoroutineHandler>
{
    /// <summary>
    /// ����Coroutine
    /// </summary>
    public static IEnumerator RunCoroutine(IEnumerator coroutine)
    {
        Instance.StartCoroutine(coroutine);
        return coroutine;
    }

    /// <summary>
    /// ����Coroutine
    /// </summary>
    public static void CancellCoroutine(IEnumerator coroutine) => Instance.StopCoroutine(coroutine);
}
