using UnityEngine;

public class Settings : MonoBehaviour
{
    public SettingsData DataToSave { get ; private set; }
    public CinemachineSensitivity SensitivitySetting;
    public SoundSettings SoundSettings;

    private void Awake()
    {
        DataToSave = SaveSystem.LoadData();
    }
    public void SaveSettings()
    {
        DataToSave.MouseSensitivity = SensitivitySetting.GetSensitivity();
        DataToSave.MasterVolume = SoundSettings.MasterSound.GetAudioVolume();
        DataToSave.SFXVolume = SoundSettings.SfxGroupSlider.GetAudioVolume();
        DataToSave.UIVolume = SoundSettings.UiGroupSlider.GetAudioVolume();
        DataToSave.SoundTrackVolume = SoundSettings.SoundTrackGroupSlider.GetAudioVolume();

        SaveSystem.SaveSettings(DataToSave);
    }

    public void LoadVolumes()
    {
        if (SoundSettings != null)
        {
            SoundSettings.MasterSound.SetMasterVolume(DataToSave.MasterVolume);
            SoundSettings.SfxGroupSlider.SetMasterVolume(DataToSave.SFXVolume);
            SoundSettings.UiGroupSlider.SetMasterVolume(DataToSave.UIVolume);
            SoundSettings.SoundTrackGroupSlider.SetMasterVolume(DataToSave.SoundTrackVolume);     
        }
    }
}
