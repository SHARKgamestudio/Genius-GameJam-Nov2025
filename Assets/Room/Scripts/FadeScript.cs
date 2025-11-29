using System.Drawing;
using UnityEngine;

public abstract class FadeScript : MonoBehaviour
{
    protected float progression = 0;
    protected UnityEngine.Color colorTarget;
    [SerializeField] protected float progressSpeed = 1;
    [SerializeField] protected UnityEngine.Color baseColor;
    protected UnityEngine.Color startColor;

    protected abstract UnityEngine.Color GetColor();
    public void SetTarget(UnityEngine.Color _color)
    {
        progression = 0;
        startColor = GetColor();
        colorTarget = _color;
    }

    public void ResetTarget()
    {
        progression = 0;
        startColor = GetColor();
        colorTarget = baseColor;
    }
}
