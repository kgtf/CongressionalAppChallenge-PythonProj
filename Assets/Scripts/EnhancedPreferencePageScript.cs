using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnhancedPreferencePageScript : MonoBehaviour
{
    public Canvas canvas;

    void Start()
    {
        if (canvas == null)
        {
            canvas = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster)).GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }

        CreatePanel();
    }

    void CreatePanel()
    {
        GameObject panel = CreateUIElement("Panel", canvas.transform, new Vector2(600, 400), Vector2.zero);
        Image panelImage = panel.AddComponent<Image>();
        panelImage.color = new Color(0.95f, 0.97f, 1f);

        CreateText("Title", panel.transform, "Refugee Resettlement Preferences", 24, FontStyle.Bold, new Vector2(0, 170));

        CreateText("Instruction", panel.transform, "Select your preferences below to find the best countries for resettlement based on climate, culture, cost of living, and more.", 14, FontStyle.Normal, new Vector2(0, 130));

        // Labels and dropdowns
        CreateLabeledDropdown(panel.transform, "Country", new Vector2(-150, 50));
        CreateLabeledDropdown(panel.transform, "Religion", new Vector2(100, 50));
        CreateLabeledDropdown(panel.transform, "Language", new Vector2(-150, -20));

        // Sliders
        CreateLabeledSlider(panel.transform, "Temperature", new Vector2(100, -20));
        CreateLabeledSlider(panel.transform, "Urbaness", new Vector2(100, -70));
        CreateLabeledSlider(panel.transform, "Distance", new Vector2(100, -120));

        // Submit button
        GameObject submit = CreateButton(panel.transform, "Submit", new Vector2(0, -170));
        submit.GetComponent<Button>().onClick.AddListener(() => Debug.Log("Submitted!"));
    }

    GameObject CreateUIElement(string name, Transform parent, Vector2 size, Vector2 anchoredPos)
    {
        GameObject go = new GameObject(name);
        RectTransform rt = go.AddComponent<RectTransform>();
        go.transform.SetParent(parent);
        rt.sizeDelta = size;
        rt.localScale = Vector3.one;
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = anchoredPos;
        return go;
    }

    void CreateText(string name, Transform parent, string text, int fontSize, FontStyle style, Vector2 pos)
    {
        GameObject textGO = CreateUIElement(name, parent, new Vector2(500, 50), pos);
        Text txt = textGO.AddComponent<Text>();
        txt.text = text;
        txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        txt.fontSize = fontSize;
        txt.alignment = TextAnchor.MiddleCenter;
        txt.fontStyle = style;
        txt.color = Color.black;
    }

    void CreateLabeledDropdown(Transform parent, string label, Vector2 pos)
    {
        CreateText(label + "Label", parent, label, 16, FontStyle.Bold, new Vector2(pos.x, pos.y + 25));
        GameObject dd = CreateUIElement(label + "Dropdown", parent, new Vector2(140, 30), pos);
        dd.AddComponent<Image>().color = Color.white;
        Dropdown dropdown = dd.AddComponent<Dropdown>();
        dropdown.options.Add(new Dropdown.OptionData("Option A"));
        dropdown.options.Add(new Dropdown.OptionData("Option B"));
    }

    void CreateLabeledSlider(Transform parent, string label, Vector2 pos)
    {
        CreateText(label + "Label", parent, label, 16, FontStyle.Bold, new Vector2(pos.x - 80, pos.y));
        GameObject sliderGO = CreateUIElement(label + "Slider", parent, new Vector2(140, 20), new Vector2(pos.x + 30, pos.y));
        Slider slider = sliderGO.AddComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = 1;
        slider.value = 0.5f;
        sliderGO.AddComponent<Image>().color = new Color(0.9f, 0.9f, 0.95f);
    }

    GameObject CreateButton(Transform parent, string buttonText, Vector2 pos)
    {
        GameObject btnGO = CreateUIElement("SubmitButton", parent, new Vector2(120, 40), pos);
        Button button = btnGO.AddComponent<Button>();
        Image img = btnGO.AddComponent<Image>();
        img.color = new Color(0.35f, 0.45f, 1f);

        GameObject textGO = CreateUIElement("ButtonText", btnGO.transform, new Vector2(120, 40), Vector2.zero);
        Text txt = textGO.AddComponent<Text>();
        txt.text = buttonText;
        txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        txt.alignment = TextAnchor.MiddleCenter;
        txt.fontSize = 16;
        txt.color = Color.white;

        return btnGO;
    }
}
