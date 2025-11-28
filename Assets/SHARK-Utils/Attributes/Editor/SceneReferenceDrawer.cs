using UnityEngine;
using UnityEditor;
using System.IO;

[CustomPropertyDrawer(typeof(SceneReferenceAttribute))]
public class SceneReferenceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SceneReferenceAttribute sceneRef = (SceneReferenceAttribute)attribute;

        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.BeginChangeCheck();

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Try to resolve the currently stored value into a SceneAsset
        Object sceneAsset = null;
        string currentValue = property.stringValue;

        if (!string.IsNullOrEmpty(currentValue))
        {
            if (sceneRef.useScenePath)
            {
                // Load by path
                sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(currentValue);
            }
            else
            {
                // Find by name (since only the name is stored)
                string[] guids = AssetDatabase.FindAssets($"{currentValue} t:Scene");
                foreach (string guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    string name = Path.GetFileNameWithoutExtension(path);
                    if (name == currentValue)
                    {
                        sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
                        break;
                    }
                }
            }
        }

        // Draw ObjectField for SceneAsset
        Object newScene = EditorGUI.ObjectField(position, sceneAsset, typeof(SceneAsset), false);

        // If changed, update serialized string
        if (EditorGUI.EndChangeCheck())
        {
            if (newScene == null)
            {
                property.stringValue = string.Empty;
            }
            else
            {
                string path = AssetDatabase.GetAssetPath(newScene);
                if (sceneRef.useScenePath)
                    property.stringValue = path;
                else
                    property.stringValue = Path.GetFileNameWithoutExtension(path);
            }
        }

        EditorGUI.EndProperty();
    }
}
