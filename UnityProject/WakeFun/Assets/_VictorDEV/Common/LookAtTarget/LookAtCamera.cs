using UnityEngine;

namespace VictorDev.Common
{
    public class LookAtCamera : LookAtTarget
    {
        override protected void OnValidate()
        {
            if (Camera.main != null) target = Camera.main.transform;
            base.OnValidate();
        }
    }
}

