using UnityEngine;
using UnityEngine.Audio;


public class SoundSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] SliderSetting MasterSound;
    [SerializeField] SliderSetting sfxGroupSlider;
    [SerializeField] SliderSetting soundTrackGroupSlider;
    [SerializeField] SliderSetting uiGroupSlider;

    //public void AdjustGroupSliderVolume(SliderSetting sliderSetting, float value)
    //{
    //    audioMixer.SetFloat(groupName, value);
    //}
}
