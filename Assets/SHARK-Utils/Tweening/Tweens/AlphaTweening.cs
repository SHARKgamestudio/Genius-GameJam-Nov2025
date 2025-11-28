using UnityEngine;

public class AlphaTweening : Tweening
{
    CanvasGroup group;

    void Start() { group = GetComponent<CanvasGroup>(); }

    override protected void Tween(float value)
    {
        group.alpha = value;
    }
}