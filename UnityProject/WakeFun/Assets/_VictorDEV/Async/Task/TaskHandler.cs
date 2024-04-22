using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Debug = UnityEngine.Debug;

/// <summary>
/// 非同步Task管理器
/// <para>+ 無法得知Task何時執行結束</para>
/// </summary>
public abstract class TaskHandler
{

    private static Dictionary<Task, CancellationTokenSource> taskCancellationDict = new Dictionary<Task, CancellationTokenSource>();

    /// <summary>
    /// 用async Task方式來執行action
    /// <para>+ 直接呼叫函式：不等待Task執行結束，直接執行下一行程式碼</para>
    /// <para>+ 加上await來呼叫函式：需等待Task執行結束，才執行下一行程式碼</para>
    /// </summary>
    /// <param name="action">欲執行的內容</param>
    /// <param name="duration">時間長度，超過則自動終止Task</param>
    public static async Task ExecuteAsync(Action action, int duration=600)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        //創建一個新的Task，並且Await
        Task task = Task.Run(async () =>
        {
            action.Invoke();
            // 模擬執行時間
            await Task.Delay(duration * 1000, cancellationToken);
        }, cancellationToken);

        taskCancellationDict[task] = cancellationTokenSource;

        try
        {
            await task;
        }
        catch (OperationCanceledException ex)
        {
            Debug.Log($"[ Task was canceled ] >>> OperationCanceledException: {ex}");
        }
        finally //最後執行
        {
            if (task.IsCompleted)
            {
                taskCancellationDict.Remove(task); //從字典中移除已完成的 Task
            }
        }
    }

    /// <summary>
    /// 終止指定Task
    /// </summary>
    /// <param name="task">指定要終止的Task</param>
    public static void CancelTask(Task task)
    {
        if(taskCancellationDict.ContainsKey(task))
       // if (taskCancellationDict.TryGetValue(task, out CancellationTokenSource cancellationTokenSource))
        {
            taskCancellationDict[task].Cancel();
            taskCancellationDict.Remove(task);
        }
        else Debug.Log($"[ CancelExecution] >>> Task not found in cancellation map.");
    }

    /// <summary>
    /// 終止全部Task
    /// </summary>
    /// <param name="task">指定要終止的Task</param>
    public static void CancelAllTask()
    {
        foreach(Task task in taskCancellationDict.Keys)
        {
            CancelTask(task);
        }
    }
}
