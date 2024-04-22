using System.Collections;
using VictorDev.Common;

/// <summary>
/// 給不是MonoBeehaviour的類別進行static呼叫使用
/// </summary>
public class CoroutineHandler : SingletonMonoBehaviour<CoroutineHandler>
{
    /// <summary>
    /// 執行Coroutine
    /// </summary>
    public static IEnumerator RunCoroutine(IEnumerator coroutine)
    {
        Instance.StartCoroutine(coroutine);
        return coroutine;
    }

    /// <summary>
    /// 取消Coroutine
    /// </summary>
    public static void CancellCoroutine(IEnumerator coroutine) => Instance.StopCoroutine(coroutine);
}
