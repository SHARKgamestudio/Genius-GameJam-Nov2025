using UnityEngine;
using UnityEngine.Rendering;

public class FadeImage : FadeScript
{
    UnityEngine.UI.Image image;

    protected override UnityEngine.Color GetColor()
    {
        return image.color;
    }

    void Start()
    {
        image = GetComponentInParent<UnityEngine.UI.Image>();
    }
    public void SetColor(UnityEngine.Color _color)
    {
        progression = 1;
        colorTarget = _color;
        image.color = colorTarget;
    }

    void Update()
    {
        if(progression < 1)
        {
            progression += progressSpeed * Time.deltaTime;
            image.color = Color.Lerp(startColor, colorTarget, progression);
        }
    }
}
