using UnityEngine;

public static class SpriteExtension
{
    /// <summary>
    /// Extension method to convert a Texture2D to a Sprite
    /// </summary>
    /// <param name="texture"></param>
    /// <returns></returns>
    public static Sprite ConvertToSprite(this Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}