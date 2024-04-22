using VictorDev.Common;
using Debug = VictorDev.Common.DebugHandler;

public class Comp4 : LifecycleComponent
{
    override public void Initialize()
    {
        Debug.Log($"Initialized: {GetType().Name}");
    }

}
