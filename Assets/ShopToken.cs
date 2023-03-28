using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopToken : MonoBehaviour
{
    public bool playerSelected;
    public string sceneName;
    private Scene currentScene;
    public void OpenShop()
    {
        StartCoroutine(LoadLevel());
    }
    public IEnumerator LoadLevel()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        StartCoroutine(PersistantData.data.TransitionIn());
        yield return new WaitForSeconds(2f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<LevelTracker>().currentLevel = sceneName;
        SceneManager.LoadScene("Shop Scene");
    }
}
