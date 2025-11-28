using UnityEngine;

public class UIRotationTweening : Tweening
{
    RectTransform transforms;
    Vector3 original;

    void Start() { transforms = GetComponent<RectTransform>(); original = transforms.localEulerAngles; }

    override protected void Tween(float value)
    {
        transforms.localRotation = Quaternion.Euler(original.x, original.y, original.z + value);
    }
}