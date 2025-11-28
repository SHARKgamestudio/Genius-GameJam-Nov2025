using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CubemapBaker))]
public class CubemapBakerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CubemapBaker baker = (CubemapBaker)target;

        if (GUILayout.Button("Bake Cubemap & Assign"))
        {
            baker.BakeCubemap();
        }
    }
}