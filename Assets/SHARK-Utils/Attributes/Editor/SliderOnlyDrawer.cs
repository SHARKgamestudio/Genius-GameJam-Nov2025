#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SliderOnlyAttribute))]
public class SliderOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SliderOnlyAttribute range = (SliderOnlyAttribute)attribute;

        EditorGUI.BeginProperty(position, label, property);

        if (property.propertyType == SerializedPropertyType.Float)
        {
            DrawFloatSlider(position, property, label, range.min, range.max);
        }
        else if (property.propertyType == SerializedPropertyType.Integer)
        {
            DrawIntSlider(position, property, label, (int)range.min, (int)range.max);
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use with float or int.");
        }

        EditorGUI.EndProperty();
    }

    private void DrawFloatSlider(Rect position, SerializedProperty property, GUIContent label, float min, float max)
    {
        // Label
        position = EditorGUI.PrefixLabel(position, label);

        // Split rect into slider + field
        Rect sliderRect = new Rect(position.x, position.y, position.width - 50, position.height);
        Rect fieldRect = new Rect(position.x + position.width - 50, position.y, 50, position.height);

        // Slider only (no built-in field!)
        float sliderValue = Mathf.Clamp(property.floatValue, min, max);
        sliderValue = GUI.HorizontalSlider(sliderRect, sliderValue, min, max);

        // Field (unclamped)
        float fieldValue = EditorGUI.FloatField(fieldRect, property.floatValue);

        // Prioritize manual input
        if (GUI.changed)
        {
            if (fieldRect.Contains(Event.current.mousePosition))
                property.floatValue = fieldValue;
            else
                property.floatValue = sliderValue;
        }
    }

    private void DrawIntSlider(Rect position, SerializedProperty property, GUIContent label, int min, int max)
    {
        position = EditorGUI.PrefixLabel(position, label);

        Rect sliderRect = new Rect(position.x, position.y, position.width - 50, position.height);
        Rect fieldRect = new Rect(position.x + position.width - 50, position.y, 50, position.height);

        int sliderValue = Mathf.Clamp(property.intValue, min, max);
        sliderValue = (int)GUI.HorizontalSlider(sliderRect, sliderValue, min, max);

        int fieldValue = EditorGUI.IntField(fieldRect, property.intValue);

        if (GUI.changed)
        {
            if (fieldRect.Contains(Event.current.mousePosition))
                property.intValue = fieldValue;
            else
                property.intValue = sliderValue;
        }
    }
}
#endif