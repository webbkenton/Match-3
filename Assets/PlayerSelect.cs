using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelect : MonoBehaviour
{
    public Animator Goldselected;
    public void StartGold()
    { 
        SceneManager.LoadScene("LevelMap");
    }
}
