using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class CinemachineSensitivity : MonoBehaviour
{
    [SerializeField] private Slider sensitivitySlider; // UI Slider
    [SerializeField] private TextMeshProUGUI valueText; // Text to display value

    private CinemachineFreeLook _freeLookCamera; 
    private void Start()
    {   
        _freeLookCamera = GameManager.Instance.FreeLookCamera; 

        SetSliderValues();
        UpdateSensitivity(sensitivitySlider.value);

        // Listen for slider value changes
        sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);
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

    private void SetSliderValues()
    {
        sensitivitySlider.minValue = 1f;
        sensitivitySlider.maxValue = 550f;

        sensitivitySlider.value = _freeLookCamera.m_XAxis.m_MaxSpeed;
    }

    private void UpdateSensitivity(float value)
    {
        _freeLookCamera.m_XAxis.m_MaxSpeed = value; // Update X-axis speed
        valueText.text = Mathf.RoundToInt(value).ToString(); // Update UI text
    }

}
