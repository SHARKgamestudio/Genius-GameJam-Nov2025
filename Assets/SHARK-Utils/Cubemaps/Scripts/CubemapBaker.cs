using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

[ExecuteInEditMode]
public class CubemapBaker : MonoBehaviour
{
    [Header("Cubemap Settings")]
    public string assetName = "RoomPreview";
    public int cubemapSize = 512;
    public string saveFolder = "Assets/Cubemaps/Output/";

    // private
    MeshRenderer mesh;

#if UNITY_EDITOR
    public void BakeCubemap()
    {
        Camera cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("No Camera component found on this GameObject!");
            return;
        }

        mesh = GetComponentInChildren<MeshRenderer>();

        if (mesh == null)
        {
            Debug.LogError("Sphere Renderer not assigned!");
            return;
        }

        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
            AssetDatabase.Refresh();
        }

        // Activate camera temporarily
        cam.enabled = true;

        // Activate mesh temporarily
        mesh.enabled = true;

        // Bake cubemap
        Cubemap cubemap = new Cubemap(cubemapSize, TextureFormat.RGBA32, false);
        cam.RenderToCubemap(cubemap);

        string cubemapPath = saveFolder + assetName + "_Cubemap.asset";
        AssetDatabase.CreateAsset(cubemap, cubemapPath);

        // Create material
        Material mat = new Material(Shader.Find("Skybox/Cubemap"));
        mat.SetTexture("_Tex", cubemap);
        string matPath = saveFolder + assetName + "_Mat.mat";
        AssetDatabase.CreateAsset(mat, matPath);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        // Apply material to sphere
        mesh.sharedMaterial = mat;

        // Restore camera state
        cam.enabled = false;

        Debug.Log($"? Cubemap baked and material assigned! \nSaved at: {cubemapPath}");
    }
#endif
}