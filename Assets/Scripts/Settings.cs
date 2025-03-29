using UnityEngine;

public class Settings : MonoBehaviour
{
    public SettingsData DataToSave { get ; private set; }
    public CinemachineSensitivity SensitivitySetting;
    public SoundSettings SoundSettings;

    private void Awake()
    {
        DataToSave = SaveSystem.LoadData();
        Debug.Log($"Sen {DataToSave.MouseSensitivity}");
    }
    public void SaveSettings()
    {
        DataToSave.MouseSensitivity = SensitivitySetting.GetSensitivity();
        SaveSystem.SaveSettings(DataToSave);
    }


}
