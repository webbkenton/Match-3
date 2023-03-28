using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopHolderScript : MonoBehaviour
{
    public void ReturnToLevel()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PointsTracker>().points++;
        StartCoroutine(LoadLevel());
    }

    public IEnumerator LoadLevel()
    {
        StartCoroutine(PersistantData.data.TransitionIn());
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(GameObject.FindGameObjectWithTag("Player").GetComponent<LevelTracker>().currentLevel);
    }
}
