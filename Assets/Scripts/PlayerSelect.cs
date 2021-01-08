using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelect : MonoBehaviour
{
    public Animator Goldselected;
    //public GameObject transition;

    private void Awake()
    {
        PersistantData.data.StartCoroutine(PersistantData.data.TransitionOut());
    }
    public void StartGold()
    {
        StartCoroutine(ChooseCharacter());
    }

    private IEnumerator ChooseCharacter()
    {
        PersistantData.data.StartCoroutine(PersistantData.data.TransitionIn());
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("LevelMap");
    }
}
