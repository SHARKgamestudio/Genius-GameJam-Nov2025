using System;
using UnityEngine;

[Serializable]
public struct TweenTransform
{
    public Vector3 position;
    public Vector3 rotation;

    public TweenTransform(Vector3 position, Vector3 rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}