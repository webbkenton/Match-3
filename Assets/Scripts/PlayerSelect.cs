using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelect : MonoBehaviour
{
    //public Animator Goldselected;
    //public GameObject transition;
    public void StartGold()
    {
        StartCoroutine(ChooseCharacter());
    }

    private IEnumerator ChooseCharacter()
    {
        PersistantData.data.StartCoroutine(PersistantData.data.TransitionIn());
        yield return new WaitForSeconds(3f);
        PersistantData.data.playerToken.SetActive(true);
        GameObject.FindGameObjectWithTag("UIManager").GetComponent<MenuScript>().CharacterSelectionUI.SetActive(false);
        GameObject.FindGameObjectWithTag("UIManager").GetComponent<MenuScript>().StartMenuHolderUI.SetActive(false);
        GameObject.FindGameObjectWithTag("UIManager").GetComponent<MenuScript>().gameStarted = true;
        SceneManager.LoadScene("NewMap");
    }
}
