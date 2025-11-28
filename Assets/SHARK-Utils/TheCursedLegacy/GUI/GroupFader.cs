using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]
public class GroupFader : MonoBehaviour
{
    [Header("Setup")]
    [Range(1, 10)]
    public float fadeTime = 2.6f;

    // Private
    CanvasGroup canvas;
    float targetAlpha;
    bool maintain;
    float ref_alpha;

    void Start() { canvas = GetComponent<CanvasGroup>(); }
    void Update()
    {
        canvas.alpha = Mathf.SmoothDamp(canvas.alpha, targetAlpha, ref ref_alpha, fadeTime/10);
        if (maintain) { targetAlpha = 0; maintain = false; }
    }

    public void Fade() { targetAlpha = 1; maintain = true; }
    public void FadeIn() { targetAlpha = 1; }
    public void FadeOut() { targetAlpha = 0; }
}
