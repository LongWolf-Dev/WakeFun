using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class RotateShape : MonoBehaviour
{
    private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    public async Task RotateForSeconds(float seconds)
    {
        float end = Time.time + seconds;
        while (Time.time < end)
        {
            transform.Rotate(new Vector3(1, 1) * Time.deltaTime * 150);
            await Task.Yield();
        }
    }

}

