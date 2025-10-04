using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Color))]
public class NamedColorPaletteDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float spacing = 4f;
        float buttonWidth = 22f;
        float lineHeight = EditorGUIUtility.singleLineHeight;

        // Имя ближайшего цвета
        string name = GetNearestColorName(property.colorValue);
        string tooltip = $"Цвет: {name}";

        // Кнопка 🎨 слева
        Rect buttonRect = new Rect(position.x, position.y, buttonWidth, lineHeight);
        if (GUI.Button(buttonRect, new GUIContent("◉", "Открыть палитру")))
        {
            Color initial = property.colorValue;
            CustomColorPickerWindow.Show(initial, newColor =>
            {
                property.colorValue = newColor;
                property.serializedObject.ApplyModifiedProperties();
            });
        }


        // ColorField справа с tooltip
        Rect colorRect = new Rect(position.x + buttonWidth + spacing, position.y,
                                  position.width - buttonWidth - spacing, lineHeight);


     // float nameWidth = 80f;
       // Rect nameRect = new Rect(position.x + buttonWidth + spacing, position.y, nameWidth, lineHeight);
        //EditorGUI.LabelField(colorRect, name, EditorStyles.miniLabel);
        //EditorGUI.LabelField(colorRect, name, EditorStyles.miniLabel);
        GUIContent tooltipLabel = new GUIContent(label.text + $" ({name})", tooltip);
        property.colorValue = EditorGUI.ColorField(colorRect, tooltipLabel, property.colorValue);

        

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }

    private string GetNearestColorName(Color color)
    {
        var palette = CustomColorPickerWindow.GetDefaultPalette();
        float minDistance = float.MaxValue;
        string closestName = "Неизвестный";

        foreach (var (knownColor, name) in palette)
        {
            float dist = Vector3.Distance(
                new Vector3(color.r, color.g, color.b),
                new Vector3(knownColor.r, knownColor.g, knownColor.b)
            );
            if (dist < minDistance)
            {
                minDistance = dist;
                closestName = name;
            }
        }

        return closestName;
    }
}
