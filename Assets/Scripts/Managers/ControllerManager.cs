using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ControllerManager : MonoBehaviour
{
    public event UnityAction OnGamepadDisconected;
    void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;

        // Handle already connected devices
        if (Gamepad.current != null)
        {
            Debug.Log("Controller already connected at startup");
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }

    void OnDisable()
    {
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
                    Cursor.visible = false;
                    break;
                case InputDeviceChange.Removed:
                    Debug.Log("Controller disconnected");
                    Cursor.visible = true;
                    OnGamepadDisconected.Invoke(); // Subscribe a Pause event
                    break;
            }
        }
    }
}
