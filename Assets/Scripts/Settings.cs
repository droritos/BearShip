using UnityEngine;

public class Settings : MonoBehaviour
{
    public SettingsData DataToSave { get ; private set; }
    public CinemachineSensitivity CinemachineSensitivity;
    private void Start()
    {
        DataToSave = SaveSystem.LoadData();
    }
    public void SaveSettings()
    {
        DataToSave.MouseSensitivity = CinemachineSensitivity.GetSensitivity();
        SaveSystem.SaveSettings(DataToSave);

        //SaveSystem.S
        //return settingsData;
    }

    //public void LoadSettings()
    //{
    //    DataToSave
    //}
}
