using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
public class CustomColorPickerWindow : EditorWindow
{
    private Color selectedColor = Color.white;
    private Action<Color> onApply;
    public static (Color, string)[] GetDefaultPalette() => colorGroups ["Популярные яркие цвета"];

    private static readonly Dictionary<string, (Color, string)[]> colorGroups = new()
    {   
        ["Популярные яркие цвета"] = new (Color, string)[]
        {
            (Color.red, "Красный"),
            (Color.green, "Зелёный"),
            (Color.blue, "Синий"),
            (Color.yellow, "Жёлтый"),
            (Color.magenta, "Пурпурный"),
            (Color.cyan, "Голубой"),
            (Color.black, "Чёрный"),
            (Color.white, "Белый"),
            (Color.gray, "Серый"),
            (new Color(1f, 0.65f, 0f), "Оранжевый"),
            (new Color(0.5f, 0f, 0.5f), "Фиолетовый"),
            (new Color(0.98f, 0.5f, 0.45f), "Коралловый"),
            (new Color(0.68f, 0.85f, 0.9f), "Небесно-голубой"),
            (new Color(0.25f, 0.88f, 0.82f), "Бирюзовый"),
            (new Color(0.13f, 0.7f, 0.67f), "Морская волна"),
            (new Color(0.93f, 0.51f, 0.93f), "Орхидея"),
            (new Color(0.86f, 0.08f, 0.24f), "Кармин"),
            (new Color(0.6f, 0.4f, 0.8f), "Лавандовый"),
            (new Color(0.75f, 0.75f, 0.75f), "Светло-серый"),
            (new Color(0.5f, 0.25f, 0f), "Коричневый")
        },

        ["Цвета для кнопок"] = new (Color, string)[]
        {
            (new Color32(0x00,0x7A,0xFF,255), "Primary"),
            (new Color32(0x33,0x9C,0xFF,255), "Primary Highlighted"),
            (new Color32(0x00,0x5F,0xCC,255), "Primary Pressed"),
            (new Color32(0xFF,0x3B,0x30,255), "Danger"),
            (new Color32(0xFF,0x5C,0x50,255), "Danger Highlighted"),
            (new Color32(0xCC,0x2E,0x25,255), "Danger Pressed"),
            (new Color32(0x34,0xC7,0x59,255), "Success"),
            (new Color32(0x5C,0xD9,0x7A,255), "Success Highlighted"),
            (new Color32(0x28,0xA7,0x45,255), "Success Pressed"),
            (new Color32(0xE0,0xE0,0xE0,255), "Secondary"),
            (new Color32(0xF0,0xF0,0xF0,255), "Secondary Highlighted"),
            (new Color32(0xC0,0xC0,0xC0,255), "Secondary Pressed"),
            (new Color32(0xA0,0xA0,0xA0,255), "Disabled")
        },

        ["Цвета интерфейса"] = new (Color, string)[]
        {
            (new Color32(0xF9,0xF9,0xF9,255), "Фон светлый"),
            (new Color32(0x1E,0x1E,0x1E,255), "Фон тёмный"),
            (new Color32(0xDD,0xDD,0xDD,255), "Граница"),
            (new Color32(0x33,0x33,0x33,255), "Текст"),
            (new Color32(0x80,0x80,0x80,255), "Разделитель"),
            (new Color32(0xFA,0xFA,0xFA,255), "Панель"),
            (new Color32(0xB0,0xB0,0xB0,255), "Иконка"),
            (new Color32(0x44,0x44,0x44,255), "Заголовок")
        },

        ["Пастельные цвета"] = new (Color, string)[]
        {
            (new Color(1f, 0.87f, 0.87f), "Розовый"),
            (new Color(0.87f, 1f, 0.87f), "Мятный"),
            (new Color(0.87f, 0.87f, 1f), "Лавандовый"),
            (new Color(1f, 1f, 0.87f), "Лимонный"),
            (new Color(0.95f, 0.85f, 0.95f), "Сиреневый"),
            (new Color(0.85f, 0.95f, 0.95f), "Бирюзовый"),
            (new Color(0.95f, 0.95f, 0.85f), "Бежевый")
        }
    };
    public static void Show(Color initial, Action<Color> onApplyCallback)
    {
        var window = GetWindow<CustomColorPickerWindow>(true, "Custom Color Picker");
        window.selectedColor = initial;
        window.onApply = onApplyCallback;
        window.minSize = new Vector2(300, 400);
        window.ShowUtility();
    }

    private void OnGUI()
    {
        GUILayout.Space(10);
        EditorGUILayout.LabelField("Выбор цвета", EditorStyles.boldLabel);

        selectedColor = EditorGUILayout.ColorField("Цвет", selectedColor);

        // HEX
        string hex = ColorUtility.ToHtmlStringRGBA(selectedColor);
        string newHex = EditorGUILayout.TextField("HEX", "#" + hex);
        if (newHex.StartsWith("#") && newHex.Length == 9)
        {
            if (ColorUtility.TryParseHtmlString(newHex, out Color parsed))
                selectedColor = parsed;
        }


        foreach (var group in colorGroups)
        {
            GUILayout.Space(10);
            EditorGUILayout.LabelField(group.Key, EditorStyles.boldLabel);

            int columns = 5;
            float boxHeight = 22f;
            var colors = group.Value;

            int rows = Mathf.CeilToInt((float)colors.Length / columns);
            for (int r = 0; r < rows; r++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int c = 0; c < columns; c++)
                {
                    int index = r * columns + c;
                    if (index >= colors.Length) break;

                    var (col, name) = colors[index];

                    GUIStyle style = new GUIStyle(GUI.skin.button)
                    {
                        fontSize = 9,
                        alignment = TextAnchor.MiddleCenter,
                        normal = { textColor = GetTextColorForBackground(col) }
                    };

                    Color old = GUI.backgroundColor;
                    GUI.backgroundColor = col;

                    Rect boxRect = GUILayoutUtility.GetRect(60, boxHeight);
                    EditorGUI.DrawRect(boxRect, col);

                    GUIStyle labelStyle = new GUIStyle(EditorStyles.label)
                    {
                        alignment = TextAnchor.MiddleCenter,
                        fontSize = 9,
                        normal = { textColor = GetTextColorForBackground(col) }
                    };

                    if (GUI.Button(boxRect, name, labelStyle))
                    {
                        selectedColor = new Color(col.r, col.g, col.b, selectedColor.a);
                    }

                    GUI.backgroundColor = old;
                }
                EditorGUILayout.EndHorizontal();
            }
        }
            
        // Кнопки
        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Применить"))
        {
            onApply?.Invoke(selectedColor);
            Close();
        }
        if (GUILayout.Button("Отмена"))
        {
            Close();
        }
        EditorGUILayout.EndHorizontal();
    }

    private Color GetTextColorForBackground(Color bg)
    {
        float luminance = 0.299f * bg.r + 0.587f * bg.g + 0.114f * bg.b;
        return luminance > 0.5f ? Color.black : Color.white;
    }
}
