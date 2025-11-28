using UnityEditor;
using UnityEngine;

public static class EditorGUICommon {

    public static bool Switch(string label, bool value) {
        string[] toolbarStrings = { "On", "Off"};
        int selected = value ? 0 : 1;

        GUILayout.BeginHorizontal();
        GUILayout.Label(label);
        selected = GUILayout.Toolbar(selected, toolbarStrings, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        value = selected == 1 ? false : true;
        return value;
    }

    public class SSValue{
        public bool enabled;
        public float value;

        public SSValue(bool visible, float value) {
            this.enabled = visible;
            this.value = value;
        }
    }
    public static SSValue SwitchSlider(string label, SSValue value)
    {
        string[] toolbarStrings = { "On", "Off" };
        int selected = value.enabled ? 0 : 1;

        GUILayout.BeginHorizontal();
        GUILayout.Label(label, GUILayout.Width(85));
        if (value.enabled) { value.value = GUILayout.HorizontalSlider(value.value, 0, 1); }
        else { value.value = 0; GUILayout.FlexibleSpace(); }
        
        selected = GUILayout.Toolbar(selected, toolbarStrings, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        value.enabled = selected == 1 ? false : true;
        return new SSValue(value.enabled, value.value);
    }

    public static bool Foldout(string label, bool value)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(10); value = EditorGUILayout.Foldout(value, label);
        EditorGUILayout.EndHorizontal();

        return value;
    }
}