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
            this.gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("PlayerHolderObject").transform);
            PersistantData.data.inEvent = true;
            this.GetComponent<SpriteRenderer>().enabled = false;
            if (transform.childCount > 0)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
            Debug.Log("Triggered");
            
            //PersistantData.data.totalCompleteLevels++;
            LoadLevel();
        }
    }
    public void LoadLevel()
    {
        StartCoroutine(PersistantData.data.TransitionIn());
        //new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMatch3");
    }
}
