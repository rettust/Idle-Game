using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;

    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }
        Debug.Log(DataHandler.creditAmount);
    }

    public void OnNewGameClicked()
    {
        DisableMenuButtons();
        // create a new game - which will initialize our game data
        DataPersistenceManager.instance.NewGame();
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("SampleScene");
        DeactivateMenu();
    }

    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        // load the next scene - which will in turn load the game because of
        // OnSceneLoaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync("SampleScene");
        DeactivateMenu();
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
