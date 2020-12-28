using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterHolder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PersistantData.data.inEvent == false)
        {
            this.gameObject.tag = "CurrentEnemy";
            PersistantData.data.inEvent = true;
            PersistantData.data.playerToken.transform.SetParent(GameObject.FindGameObjectWithTag("PersistantData").transform);
            //PersistantData.data.playerToken.SetActive(false);
            Debug.Log("Triggered");
            //PersistantData.data.totalCompleteLevels++;
            LoadLevel("MainMatch3");
        }
    }
    public void LoadLevel(string MainMatch3)
    {
        SceneManager.LoadScene(MainMatch3);
    }
}
