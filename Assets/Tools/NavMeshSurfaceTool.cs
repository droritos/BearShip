using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;

public class NavMeshSurfaceTool : EditorWindow
{
    private NavMeshSurface[] navMeshSurfaces;

    // Add this menu item in Unity's Tools menu
    [MenuItem("Tools/NavMesh Surface Manager")]
    public static void ShowWindow()
    {
        // Create and show the editor window using this actual class "NavMeshSurfaceTool" 
        GetWindow<NavMeshSurfaceTool>("NavMesh Surface Manager");
    }

    // Draw the window GUI menu in unity
    private void OnGUI()
    {
        GUILayout.Label("Quick way for those surfaces", EditorStyles.boldLabel);

        if (GUILayout.Button("Clear And Rebake All NavMeshes "))
        {
            FindNavMeshSurfaces();
            ClearAllNavMeshes();
            BakeAllNavMeshes();
        }

        GUILayout.Label("Individual Button", EditorStyles.boldLabel);

        if (GUILayout.Button("Find All NavMesh Surfaces"))
        {
            FindNavMeshSurfaces();
        }

        if (GUILayout.Button("Clear All NavMeshes"))
        {
            ClearAllNavMeshes();
        }

        if (GUILayout.Button("Bake All NavMeshes"))
        {
            BakeAllNavMeshes();
        }


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("NavMesh Surfaces Found:", navMeshSurfaces != null ? navMeshSurfaces.Length.ToString() : "0");
    }

    private void FindNavMeshSurfaces()
    {
        navMeshSurfaces = FindObjectsByType<NavMeshSurface>(FindObjectsSortMode.InstanceID);
        Debug.Log($"Found {navMeshSurfaces.Length} NavMesh Surfaces.");
    }

    private void ClearAllNavMeshes()
    {
        if (navMeshSurfaces == null || navMeshSurfaces.Length == 0)
        {
            Debug.LogWarning("No NavMesh surfaces found! Make sure to find them first.");
            return;
        }

        foreach (NavMeshSurface surface in navMeshSurfaces)
        {
            if (surface.navMeshData != null)
            {
                // Remove and reset NavMeshSurface data
                surface.RemoveData();
                surface.navMeshData = null; // Nullify the reference
                Debug.Log($"Cleared NavMesh for {surface.gameObject.name}");
            }
        }

        Debug.Log("Cleared all NavMesh surfaces.");
    }

    private void BakeAllNavMeshes()
    {
        if (navMeshSurfaces == null || navMeshSurfaces.Length == 0)
        {
            Debug.LogWarning("No NavMesh surfaces found! Make sure to find them first.");
            return;
        }

        foreach (NavMeshSurface surface in navMeshSurfaces)
        {
            surface.BuildNavMesh();
        }

        Debug.Log("Baked all NavMesh surfaces.");
    }
}
