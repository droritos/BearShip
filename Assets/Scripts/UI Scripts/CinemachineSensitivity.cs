using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class CinemachineSensitivity : SliderSetting
{
    private CinemachineFreeLook _freeLookCamera;
    
    protected override void Start()
    {   
        _freeLookCamera = GameManager.Instance.FreeLookCamera;
        valueSetting = GetSensitivity();
        base.Start();
    }

    public float GetSensitivity()
    {
        if (_freeLookCamera != null)
        {
            return _freeLookCamera.m_XAxis.m_MaxSpeed; 
        }
        else
        {
            return GameManager.Instance.FreeLookCamera.m_XAxis.m_MaxSpeed;
        }
    }

    protected override void SetSliderValues(float maxValue, float valueSetting = 1)
    {
        SliderObject.minValue = 1f;
        SliderObject.maxValue = maxValue;

        UpdateDisplay(valueSetting);
    }

    public override void UpdateDisplay(float value)
    {
        if (_freeLookCamera != null)
        {
            _freeLookCamera.m_XAxis.m_MaxSpeed = value; // Update X-axis speed
        }
        SliderObject.value = value;
        valueText.text = Mathf.RoundToInt(value).ToString(); // Update UI text
    }

}
