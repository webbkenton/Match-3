using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterHolder : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        LoadLevel("MainMatch3");
    }
    public void LoadLevel(string MainMatch3)
    {
        SceneManager.LoadScene(MainMatch3);
    }
}
