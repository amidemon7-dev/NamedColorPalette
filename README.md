
# 🎨 NamedColorPaletteDrawer & CustomColorPickerWindow

Минималистичный и расширяемый редактор выбора цвета для Unity Inspector. Позволяет быстро выбирать цвета из палитры, видеть их названия, и работать с UI‑состояниями, темами и визуальными акцентами.

---
<img width="277" height="122" alt="image" src="https://github.com/user-attachments/assets/e41741e5-fc20-46f2-a7a9-9efb2548e765" />
<img width="245" height="392" alt="image" src="https://github.com/user-attachments/assets/b80f8940-58b2-49d1-acb6-71b569a49643" />

## ✨ Возможности

- 🔘 **Кнопка вызова палитры** рядом с `ColorField` — компактная иконка или текст
- 🎨 **Расширенное окно выбора цвета** (`CustomColorPickerWindow`) с группами:
  - Популярные яркие цвета
  - Цвета для кнопок (Primary, Danger, Success и т.д.)
  - Цвета интерфейса (фон, текст, границы)
  - Пастельные и нейтральные оттенки
- 🧠 **Автоматическое определение имени цвета** — отображается рядом с полем и в tooltip
- 🧾 **HEX‑ввод** и отображение HEX‑кода при наведении
- 🧩 Поддержка `SerializedProperty` — работает с любыми `Color` полями, включая `Button.colors`
- ⚡ Совместимо с тёмной и светлой темой Unity

---

## 🛠 Установка

1. Скопируйте `NamedColorPaletteDrawer.cs` и `CustomColorPickerWindow.cs` в папку `Editor/`
2. Используйте `Color` поля в вашем `MonoBehaviour` или `ScriptableObject`
3. Палитра откроется по кнопке 🎨 в инспекторе

---

## 📦 Пример использования

```csharp
public class MyComponent : MonoBehaviour
{
    public Color buttonColor;
}
```

---

## 📌 Советы

- Используйте `EditorGUIUtility.IconContent("ColorPicker")` для стабильной иконки
- Названия цветов определяются по ближайшему совпадению с палитрой
- HEX‑код можно скопировать вручную или отобразить в tooltip

---

## 🧠 Авторские идеи

Разработано для дизайнеров, UI‑разработчиков и тех, кто ценит чистый инспектор, быстрый доступ к цветам и визуальную ясность.
