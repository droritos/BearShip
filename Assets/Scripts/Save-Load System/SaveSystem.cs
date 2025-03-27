using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static string SavePath = Application.persistentDataPath + "/savefile.json";

    public static void SaveSettings(SettingsData settingsData)
    {
        string json = JsonUtility.ToJson(settingsData, true); // `true` makes it pretty-printed
        File.WriteAllText(SavePath, json);
        Debug.Log("Setting Saved to: " + SavePath);
    }

    public static SettingsData LoadData()
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("Save file not found, creating new data.");
            return new SettingsData();
        }
        else
        {
            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<SettingsData>(json);
        }
    }
}
