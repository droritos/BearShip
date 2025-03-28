using UnityEngine;

public class Settings : MonoBehaviour
{
    public SettingsData DataToSave { get ; private set; }
    public CinemachineSensitivity CinemachineSensitivity;
    private void Awake()
    {
        DataToSave = SaveSystem.LoadData();
        Debug.Log($"Sen {DataToSave.MouseSensitivity}");
    }
    public void SaveSettings()
    {
        DataToSave.MouseSensitivity = CinemachineSensitivity.GetSensitivity();
        SaveSystem.SaveSettings(DataToSave);
    }

}
