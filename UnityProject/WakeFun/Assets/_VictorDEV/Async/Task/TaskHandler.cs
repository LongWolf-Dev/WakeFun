using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Debug = UnityEngine.Debug;

/// <summary>
/// �D�P�BTask�޲z��
/// <para>+ �L�k�o��Task��ɰ��浲��</para>
/// </summary>
public abstract class TaskHandler
{

    private static Dictionary<Task, CancellationTokenSource> taskCancellationDict = new Dictionary<Task, CancellationTokenSource>();

    /// <summary>
    /// ��async Task�覡�Ӱ���action
    /// <para>+ �����I�s�禡�G������Task���浲���A��������U�@��{���X</para>
    /// <para>+ �[�Wawait�өI�s�禡�G�ݵ���Task���浲���A�~����U�@��{���X</para>
    /// </summary>
    /// <param name="action">�����檺���e</param>
    /// <param name="duration">�ɶ����סA�W�L�h�۰ʲפ�Task</param>
    public static async Task ExecuteAsync(Action action, int duration=600)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        //�Ыؤ@�ӷs��Task�A�åBAwait
        Task task = Task.Run(async () =>
        {
            action.Invoke();
            // ��������ɶ�
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
        finally //�̫����
        {
            if (task.IsCompleted)
            {
                taskCancellationDict.Remove(task); //�q�r�夤�����w������ Task
            }
        }
    }

    /// <summary>
    /// �פ���wTask
    /// </summary>
    /// <param name="task">���w�n�פTask</param>
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
    /// �פ����Task
    /// </summary>
    /// <param name="task">���w�n�פTask</param>
    public static void CancelAllTask()
    {
        foreach(Task task in taskCancellationDict.Keys)
        {
            CancelTask(task);
        }
    }
}
