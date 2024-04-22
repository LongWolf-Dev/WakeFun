using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TaskHandler_Test : MonoBehaviour
{
    [SerializeField] private Text counterText;

    private int counter = 0;
    private Task currentTask;

    public void StartTask()
    {
        currentTask = TaskHandler.ExecuteAsync(CountAdd);
    }

    private void Update()
    {
        counterText.text = counter.ToString();
    }


    private async void CountAdd()
    {
        if(currentTask != null) TaskHandler.CancelTask(currentTask);
        while (true)
        {
            counter++;
            await Task.Delay(1000);
        }
    }

}
