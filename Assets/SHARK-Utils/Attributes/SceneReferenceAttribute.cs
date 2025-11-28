using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Attribute for displaying a Scene asset picker that stores the scene name or path as a string.
/// </summary>
public class SceneReferenceAttribute : PropertyAttribute
{
    public bool useScenePath;

    public SceneReferenceAttribute(bool useScenePath = false)
    {
        this.useScenePath = useScenePath;
    }
}