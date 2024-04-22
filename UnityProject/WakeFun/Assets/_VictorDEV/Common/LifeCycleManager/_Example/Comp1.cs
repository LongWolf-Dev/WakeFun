using VictorDev.Common;
using Debug = VictorDev.Common.DebugHandler;

public class Comp1 : LifecycleComponent
{
     override public void Initialize()
    {
        Debug.Log($"Initialized: {GetType().Name}");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
