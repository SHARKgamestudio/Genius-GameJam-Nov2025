using UnityEngine;

public class Actuator : MonoBehaviour
{
    [SerializeField] protected Tweening[] tweens;

    protected void Tween()
    {
        foreach (Tweening tween in tweens)
        { tween.Hold();  }
    }

    protected void TweenIn()
    {
        foreach (Tweening tween in tweens)
        { tween.In(); }
    }

    protected void TweenOut()
    {
        foreach (Tweening tween in tweens)
        { tween.Out(); }
    }
}