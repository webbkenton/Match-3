using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject leftPath;
    //public GameObject middlePath;
    //public GameObject rightPath;
    private GameObject player;
    public GameObject map;
    public GameObject[] level1Monsters;
    void Start()
    {
        player = PersistantData.data.playerToken;
    }
    void Update()
    {
        KeepThePath();
    }

    private void KeepThePath()
    {
        if (player != null)
        {
            if (player.GetComponent<LevelTracker>().levelInProgress)
            {
                map = GameObject.FindGameObjectWithTag("Map");
                DontDestroyOnLoad(map);
            }
        }
    }
    public void RestoreTheMap()
    {
        map.transform.localScale = new Vector3(.7f, .7f, 0f);
        player.transform.localScale = new Vector3(1, 1, 0);
    }
}
