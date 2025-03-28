using UnityEngine;

public class PopUpsHandler : MonoBehaviour
{
    [SerializeField] private GameObject winPopUp;
    [SerializeField] private GameObject losePopUp;

    public void OnWin()
    {
        winPopUp.SetActive(true);
    }
    public void OnLose()
    {
        losePopUp.SetActive(true);
    }

    public void OnButtonPressed()
    {
        if (winPopUp.activeSelf)
            winPopUp.SetActive(false);
        if (losePopUp.activeSelf)
            losePopUp.SetActive(false);
    }

    public void OnReturnToMainMenuPressed()
    {
        Debug.Log("Return to Main Menu Pressed");
        GameManager.Instance.BackToMainMenu();
    }
}
