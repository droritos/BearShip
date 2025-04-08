using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuHandler : MonoBehaviour
{
    public event UnityAction<bool> OnPause;

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
            //Time.timeScale = 0f;
        }
        else
        {
            //Time.timeScale = 1.0f;
        }
        OnPause?.Invoke(OptionsMenu.activeSelf);

        SoundManager.Instance.PlayUISound(pauseClip, transform); // Do Pause / Unpause SFX

    }
}
