using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPrefsTool : EditorWindow
{
    [SerializeField] public UnityEvent ReSetPlayerPrefs; 
    
    // Add this menu item in Unity's Tools menu
    [MenuItem("Tools/PlayerPrefsTool")]
    public static void ShowWindow()
    {
        // Create and show the editor window using this actual class "NavMeshSurfaceTool" 
        GetWindow<PlayerPrefsTool>("PlayerPrefsTool");
    }

    // Draw the window GUI menu in unity
    private void OnGUI()
    {
        GUILayout.Label("Reset All Player Prefs", EditorStyles.boldLabel);

        if (GUILayout.Button("Reset"))
        {
            ResetPlayerPrefs();
        }
    }

    private void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
