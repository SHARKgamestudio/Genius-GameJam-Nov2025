using UnityEngine;

public class DebugActuator : Actuator
{
    [Space]

    [SerializeField] bool enable;

    void Update()
    {
        if(enable) { TweenIn(); }
        else { TweenOut(); }
    }
}