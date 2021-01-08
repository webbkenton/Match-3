using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(TimeStart());
    }
    public IEnumerator TimeStart()
    {
        PersistantData.data.StartCoroutine(PersistantData.data.TransitionIn());
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("ChooseYourPath");
    }
}
