using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VictorDev.EditorTool;

public class CoroutineHandler_Test : MonoBehaviour
{
    private List<IEnumerator> coroutines = new List<IEnumerator>();

    private void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            IEnumerator counter = CounterCoroutine($"Counter-{i}");
            coroutines.Add(counter);
            CoroutineHandler.RunCoroutine(counter);
        }
    }

    private IEnumerator CounterCoroutine(string name)
    {
        int counter = 0;
        while (true)
        {
            yield return new WaitForSeconds(1);
            Debug.Log($"CounterCoroutine: {name} / {counter++}");
        }
    }
    public void CancellCoroutine()
    {
        Debug.Log(">>> Stop Coroutine");
        coroutines.ForEach(coroutine => CoroutineHandler.CancellCoroutine(coroutine));
    }

    [CustomEditor(typeof(CoroutineHandler_Test))]
    private class Inspector : InspectorEditor<CoroutineHandler_Test>
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUIStyle btnStyle = _CreateButtonStyle();
            _CreateButton("StopCoroutine", btnStyle, () => instance.CancellCoroutine());
        }
    }

}

