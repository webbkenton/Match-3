using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldMap : MonoBehaviour
{
    private GameObject playerToken;
    void Start()
    {
        playerToken = GameObject.FindGameObjectWithTag("Player");
    }

    public void MovePlayerToken()
    {
        playerToken.transform.SetParent(this.transform); 
        Mathf.Lerp(playerToken.transform.position.y, this.transform.position.y, .1f);
    }
}
