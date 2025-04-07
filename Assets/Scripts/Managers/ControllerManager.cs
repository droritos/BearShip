using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerManager : MonoBehaviour
{
    void OnEnable()
    {
        // Subscribe to device change events
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    void OnDisable()
    {
        // Unsubscribe to avoid potential errors
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (device is Gamepad)
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    Debug.Log("Controller connected");
                    Cursor.visible = false; // Hide the cursor if needed
                    break;
                case InputDeviceChange.Removed:
                    Debug.Log("Controller disconnected");
                    Cursor.visible = true; // Show the cursor
                    break;
            }
        }
    }
}