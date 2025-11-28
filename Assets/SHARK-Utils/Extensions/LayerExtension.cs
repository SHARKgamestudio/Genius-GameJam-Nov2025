using UnityEngine;

public static class LayerExtension
{
    /// <summary>
    /// Extension method to check if a layer is in a layermask
    /// </summary>
    /// <param name="mask"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    /// <summary>
    /// Extension method that returns a layer mask including all layers except the specified one
    /// </summary>
    /// <param name="mask"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    public static LayerMask Exclude(this LayerMask mask, int layer)
    {
        return ~(1 << layer);
    }
}