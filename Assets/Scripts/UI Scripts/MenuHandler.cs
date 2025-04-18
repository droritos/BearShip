using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuHandler : MonoBehaviour
{
    public event UnityAction<bool> OnPausedMenu;

    [Header("Menus")]
    public GameObject OptionsMenu;

    [Header("Buttons Managemnet")]
    [SerializeField] GameObject menuFirstButton;
    [SerializeField] GameObject settingsFirstObject;
    [SerializeField] GameObject optionCloseButton;

    [SerializeField] AudioClip pauseClip;


    public void SettingsMenu()
    {
        SetCurrentSelectedObject(settingsFirstObject); // The first opbject is the slider 
    }
    public void CloseOption()
    {
        SetCurrentSelectedObject(optionCloseButton);
    }
    public void SetCurrentSelectedObject(GameObject wantedGameObject)
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

        OnPausedMenu?.Invoke(OptionsMenu.activeSelf);

        SoundManager.Instance.PlayUISound(pauseClip, transform); // Do Pause / Unpause SFX

    }
}
