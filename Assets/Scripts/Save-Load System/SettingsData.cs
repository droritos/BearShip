using UnityEngine;

public class SettingsData
{
    public float MouseSensitivity;
    public float MasterVolume;
    public float SFXVolume;
    public float SoundTrackVolume;
    public float UIVolume;
    
    public SettingsData()
    {
        MouseSensitivity = 300f;
        MasterVolume = 1.0f;
        SFXVolume = 1.0f;
        SoundTrackVolume = 1.0f;
        UIVolume = 1.0f;
    }
}
