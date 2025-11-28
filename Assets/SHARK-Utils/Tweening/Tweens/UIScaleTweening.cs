using UnityEngine;

public class UIScaleTweening : Tweening
{
    RectTransform transforms;
    Vector3 original;

    void Start() { transforms = GetComponent<RectTransform>(); original = transforms.localScale; }

    override protected void Tween(float value)
    {
        transforms.localScale = original + new Vector3(value, value, value);
    }
}