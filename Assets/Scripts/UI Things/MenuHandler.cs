using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuHandler : MonoBehaviour
{
    [Header("Menus")]
    public GameObject OptionsMenu;

    [Header("Buttons Managemnet")]
    [SerializeField] GameObject menuFirstButton;
    [SerializeField] GameObject settingsFirstObject;
    [SerializeField] GameObject optionCloseButton;


    public void SettingsMenu()
    {
        SetCurrentSelectedObject(settingsFirstObject); // The first opbject is the slider 
    }
    public void CloseOption()
    {
        SetCurrentSelectedObject(optionCloseButton);
    }
    private void SetCurrentSelectedObject(GameObject wantedGameObject)
    {
        // Clear Selected Object
        EventSystem.current.SetSelectedGameObject(null);
        // Set New Selected Object
        EventSystem.current.SetSelectedGameObject(wantedGameObject);
    }

    public void OpenPauseMenu(InputAction.CallbackContext context)
    {
        ToggleOptionsMenu();
    }

    public void ToggleOptionsMenu()
    {
        bool isActive = !OptionsMenu.activeSelf;
        OptionsMenu.SetActive(isActive);

        if (isActive)
        {
            SetCurrentSelectedObject(menuFirstButton);
        }

        UpdateTimeScale();
    }

    private void UpdateTimeScale()
    {
        Time.timeScale = HasAnyMenuActive() ? 0f : 1f;
    }

    private bool HasAnyMenuActive()
    {
        foreach (RectTransform child in transform)
        {
            Debug.Log("Checking: " + child.gameObject.name + " | Active: " + child.gameObject.activeSelf);
            if (child.gameObject.activeSelf)
                return true;
        }
        return false;
    }
}
