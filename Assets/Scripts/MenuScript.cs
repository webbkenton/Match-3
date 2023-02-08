using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject WarningText;
    public GameObject StartMenuHolderUI;
    public GameObject CharacterSelectionUI;
    public GameObject Match3UI;
    public GameObject Match3Board;
    public bool gameStarted;

    private void Start()
    {
        
        //StartMenuMap();
    }
    public void StartGame()
    {
        if (gameStarted == false)
        { CharacterSelectionUI.SetActive(true); }
    }
    public void QuitGameWarning()
    {
        WarningText.SetActive(true);
    }
    public void QuitGame()
    { Application.Quit(); }
    public void ReturnToGame()
    { WarningText.SetActive(false); }

    private void StartMenuMap()
    {
        if (gameStarted == false)
        {
            StartMenuHolderUI.SetActive(true);
        }
        else
        {
            StartMenuHolderUI.SetActive(false);
        }
    }
}
