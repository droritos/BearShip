using UnityEngine;
using UnityEngine.Audio;


public class SoundSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public MixerController MasterSound;
    public MixerController SfxGroupSlider;
    public MixerController UiGroupSlider;
    public MixerController SoundTrackGroupSlider;
}
