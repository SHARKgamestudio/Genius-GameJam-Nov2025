using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] Axis orientation = Axis.X;
    [Range(0,1)]
    public float value;

    enum Axis { X, Y, Z }

    [Header("References")]
    [SerializeField] RectTransform rectTransform;

    void Update()
    {
        rectTransform = GetComponent<RectTransform>();

        if (orientation == Axis.X) { rectTransform.localScale = new Vector3(value, rectTransform.localScale.y, rectTransform.localScale.z); }
        if (orientation == Axis.Y) { rectTransform.localScale = new Vector3(rectTransform.localScale.x, value, rectTransform.localScale.z); }
        if (orientation == Axis.Z) { rectTransform.localScale = new Vector3(rectTransform.localScale.x, rectTransform.localScale.y, value); }
        
    }
}
