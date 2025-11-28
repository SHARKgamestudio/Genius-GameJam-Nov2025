using UnityEngine;

public class PositionTweening : Tweening
{
    public enum Axis { X, Y, Z }

    [SerializeField] Axis axis = Axis.Y;

    // Privates
    Vector3 original;

    void Start() { original = transform.localPosition; }

    override protected void Tween(float value)
    {
        if (axis == Axis.X)
            transform.localPosition = original + new Vector3(value, 0, 0);
        else if (axis == Axis.Y)
            transform.localPosition = original + new Vector3(0, value, 0);
        else if (axis == Axis.Z)
            transform.localPosition = original + new Vector3(0, 0, value);
    }
}