using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField] AudioMixer myAudioMixer;
    [SerializeField] TextMeshProUGUI valueText;
    [SerializeField] Slider slider;
    private float _currentVolume;
    [SerializeField] private SoundMixerType myMixerType;

    public const string SFX = "MA_SFX";
    public const string SoundTrack = "MA_SoundTrack";
    public const string UI = "MA_UI";
    public const string MasterVolume = "MA_Master";
    public const string FF = "F2";

    public float GetAudioVolume()
    {
        float dbValue = 0f;
        string parameterName = "";

        switch (myMixerType)
        {
            case SoundMixerType.Master:
                parameterName = MasterVolume;
                break;
            case SoundMixerType.SFX:
                parameterName = SFX;
                break;
            case SoundMixerType.SoundTrack:
                parameterName = SoundTrack;
                break;
            case SoundMixerType.UI:
                parameterName = UI;
                break;
        }

        myAudioMixer.GetFloat(parameterName, out dbValue);
        // Convert dB back to linear value (0-1)
        return dbValue > -80f ? Mathf.Pow(10, dbValue / 20f) : 0f;
    }


    public void SetMasterVolume(float sliderValue)
    {
        myMixerType = SoundMixerType.Master;
        slider.value = sliderValue;

        // Handle edge case where sliderValue is 0 to avoid -Infinity from Log10(0)
        float dbValue = sliderValue > 0.0001f ? (Mathf.Log10(sliderValue) * 20) : -80f;
        myAudioMixer.SetFloat(MasterVolume, dbValue);

        // Update UI text to show percentage
        _currentVolume = (sliderValue * 100);
        string percentage = _currentVolume.ToString("F2") + "%";
        valueText.text = percentage;
    }

    public void SetSFXVolume(float sliderValue)
    {
        myMixerType = SoundMixerType.SFX;
        slider.value = sliderValue;

        float dbValue = sliderValue > 0.0001f ? (Mathf.Log10(sliderValue) * 20) : -80f;
        myAudioMixer.SetFloat(SFX, dbValue);

        _currentVolume = (sliderValue * 100);
        string percentage = _currentVolume.ToString("F2") + "%";
        valueText.text = percentage;
    }

    public void SetUIVolume(float sliderValue)
    {
        myMixerType = SoundMixerType.UI;
        slider.value = sliderValue;

        float dbValue = sliderValue > 0.0001f ? (Mathf.Log10(sliderValue) * 20) : -80f;
        myAudioMixer.SetFloat(UI, dbValue);

        _currentVolume = (sliderValue * 100);
        string percentage = _currentVolume.ToString("F2") + "%";
        valueText.text = percentage;
    }

    public void SetSoundTrackVolume(float sliderValue)
    {
        myMixerType = SoundMixerType.SoundTrack;
        slider.value = sliderValue;

        float dbValue = sliderValue > 0.0001f ? (Mathf.Log10(sliderValue) * 20) : -80f;
        myAudioMixer.SetFloat(SoundTrack, dbValue);

        _currentVolume = (sliderValue * 100);
        string percentage = _currentVolume.ToString("F2") + "%";
        valueText.text = percentage;
    }
    private void SetVolumeBasedOnType(float sliderValue)
    {
        switch (myMixerType)
        {
            case SoundMixerType.Master:
                SetMasterVolume(sliderValue);
                break;
            case SoundMixerType.SFX:
                SetSFXVolume(sliderValue);
                break;
            case SoundMixerType.SoundTrack:
                SetSoundTrackVolume(sliderValue);
                break;
            case SoundMixerType.UI:
                SetUIVolume(sliderValue);
                break;
        }
    }

}