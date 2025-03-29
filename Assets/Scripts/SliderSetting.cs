using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderSetting : MonoBehaviour
{
    public Slider SliderObject; // UI Slider
    [SerializeField] protected TextMeshProUGUI valueText; // Text to display value
    [SerializeField] protected float _maxValue = 550f;
    protected float valueSetting { get; set; }

    private delegate void SliderValueChangedDelegate(float value);

    protected virtual void Start()
    {
        SetSliderValues(_maxValue, valueSetting);
        UpdateSentting(valueSetting);

        // Listen for slider value changes
        SliderObject.onValueChanged.AddListener(UpdateSentting);
    }
    protected virtual void SetSliderValues(float maxValue, float valueSetting = 1)
    {
        SliderObject.minValue = 1f;
        SliderObject.maxValue = maxValue;

        SliderObject.value = valueSetting;
    }

    protected virtual void UpdateSentting(float settingValue)
    {
        settingValue = SliderObject.value; // Update X-axis speed
        valueText.text = Mathf.RoundToInt(SliderObject.value).ToString(); // Update UI text
    }

}
