using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField] AudioMixer myAudioMixer;
    [SerializeField] TextMeshProUGUI valueText;

    private float _currentVolume;
    //public int AudioVolume { get { return myAudioMixer.vol; } }
    public const string SFX = "MA_SFX";
    public const string SoundTrack = "MA_SoundTrack";
    public const string UI = "MA_UI";
    public const string MasterVolume = "MA_Master";
    public const string FF = "F2";

    private void Start()
    {
        valueText.text = "50%";
    }
    public float GetAudioVolume()
    {
        return _currentVolume;
    }
    public void SetMasterVolume(float sliderValue)
    {
        myAudioMixer.SetFloat(MasterVolume, (MathF.Log10(sliderValue) * 20));

        // Update UI text to show percentage
        _currentVolume = (sliderValue * 100);
        string percentage = _currentVolume.ToString("F2") + "%";
        valueText.text = percentage;
    }
    public void SetSFXVolume(float sliderValue)
    {
        myAudioMixer.SetFloat(SFX, (MathF.Log10(sliderValue) * 20));

        _currentVolume = (sliderValue * 100);
        string percentage = _currentVolume.ToString("F2") + "%";
        valueText.text = percentage;
    }
    public void SetUIVolume(float sliderValue)
    {
        myAudioMixer.SetFloat(UI, (MathF.Log10(sliderValue) * 20));

        _currentVolume = (sliderValue * 100);
        string percentage = _currentVolume.ToString("F2") + "%";
        valueText.text = percentage;
    }
    public void SetSoundTrackVolume(float sliderValue)
    {
        myAudioMixer.SetFloat(SoundTrack, (MathF.Log10(sliderValue) * 20));

        _currentVolume = (sliderValue * 100);
        string percentage = _currentVolume.ToString("F2") + "%";
        valueText.text = percentage;
    }


}
