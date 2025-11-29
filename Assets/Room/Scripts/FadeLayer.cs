using UnityEngine;
using UnityEngine.Rendering;

public class FadeLayer : FadeScript
{
    SpriteRenderer spriteRenderer;

    protected override UnityEngine.Color GetColor()
    {
        return spriteRenderer.color;
    }

    void Start()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }
    public void SetColor(UnityEngine.Color _color)
    {
        progression = 1;
        colorTarget = _color;
        spriteRenderer.color = colorTarget;
    }
    void Update()
    {
        if(progression < 1)
        {
            progression += progressSpeed * Time.deltaTime;
            spriteRenderer.color = Color.Lerp(startColor, colorTarget, progression);
        }
    }
}
