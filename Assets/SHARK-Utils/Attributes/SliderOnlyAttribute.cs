using UnityEngine;

public class SliderOnlyAttribute : PropertyAttribute
{
    public readonly float min;
    public readonly float max;

    public SliderOnlyAttribute(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}