using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RotateShapeTest : MonoBehaviour
{
    [SerializeField] private List<RotateShape> _shapes;
    [SerializeField] private Text _finishedText;

    /// <summary>
    /// 並列執行
    /// </summary>
    public async void GoConcurrent()
    {
        _finishedText.gameObject.SetActive(false);

        var tasks = new List<Task>();
        int counter = 0;
        _shapes.ForEach(rotateShape =>
        {
            tasks.Add(rotateShape.RotateForSeconds(1 + 1 * counter++));
        });

        //等待指定任務結束
        await Task.WhenAll(tasks);
        _finishedText.gameObject.SetActive(true);
    }

    /// <summary>
    /// 串列執行
    /// </summary>
    public async void GoSequence()
    {
        _finishedText.gameObject.SetActive(false);
        for (int i = 0; i < _shapes.Count; i++)
        {

            await _shapes[i].RotateForSeconds(1 + 1 * i);
        }
        _finishedText.gameObject.SetActive(true);
    }

    /// <summary>
    /// 擷取亂數
    /// </summary>
    public async void Test_RandomNumer()
    {
        var task = GetRandomNumber();
        await task;
        Debug.Log($"Result: {task.Result}");

        var taskGetRandomNumber = await GetRandomNumber();
        Debug.Log(taskGetRandomNumber);
    }

    private async Task<int> GetRandomNumber()
    {

        int randomNumber = Random.Range(100, 300);
        await Task.Delay(randomNumber);
        return randomNumber;
    }

}
