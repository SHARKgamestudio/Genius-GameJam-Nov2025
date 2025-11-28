using UnityEngine.UI;

public class ImageFillTweening : Tweening
{
    Image image;

    void Start() { image = GetComponent<Image>(); }

    override protected void Tween(float value)
    {
        image.fillAmount = value;
    }
}