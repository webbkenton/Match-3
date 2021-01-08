using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverWorldMap : MonoBehaviour
{
    private GameObject playerToken;
    public bool objectiveComplete;
    public Transform playerTransform;
    private PlayerTokenMover mover;

    private void Awake()
    {
        //StartCoroutine(PersistantData.data.TransitionOut());
    }
    void Start()
    {
        playerToken = GameObject.FindGameObjectWithTag("Player");
        mover = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTokenMover>();
    }

    private void Update()
    {
        playerToken = GameObject.FindGameObjectWithTag("Player");
    }
    public void MovePlayerToken()
    {
        playerToken.transform.SetParent(this.transform);
        PersistantData.data.currentObjective = this.gameObject;
        Mathf.Lerp(playerToken.transform.position.y, this.transform.position.y, .1f);
        PersistantData.data.waitForMove = false;
    }


}
