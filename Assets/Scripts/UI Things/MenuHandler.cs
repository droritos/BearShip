using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuHandler : MonoBehaviour
{
    [Header("Menus")]
    public GameObject OptionsMenu;

    [Header("Buttons Managemnet")]
    [SerializeField] GameObject menuFirstButton;
    [SerializeField] GameObject settingsFirstButton;
    [SerializeField] GameObject optionCloseButton;


    public void SettingsMenu()
    {
        SetCurrentSelectedObject(settingsFirstButton);
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
        OptionsMenu.SetActive(!OptionsMenu.activeSelf);

        if (OptionsMenu.activeSelf)
        {
            SetCurrentSelectedObject(menuFirstButton);
            Time.timeScale = 0f;
        }
        else
            Time.timeScale = 1.0f;

    }


}
