using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTest : MonoBehaviour
{
    private Transform targetPosition;
    private bool moving;
    private bool firstMove;
    public bool inCombat;
    public GameObject enemyObject;
    private GameObject player;
    public string selectedColumn;
    public GameObject defaultTokenHolder;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        defaultTokenHolder = this.gameObject;
        selectedColumn = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !inCombat)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null)
            {
                player.GetComponent<LevelTracker>().levelInProgress = true;
                enemyObject = hit.transform.gameObject;
                moving = true;
                targetPosition = hit.transform;
                if (enemyObject.GetComponent<FightTrigger>() != null)
                {
                    enemyObject.GetComponent<FightTrigger>().playerSelected = true;
                }
            }
        }
        WhenToMove();
        CheckForMove();
        if (enemyObject != null && moving && enemyObject.GetComponent<AllowMovement>().movementAllowed)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition.position, 10f * Time.deltaTime);
        }
    }
    private void WhenToMove()
    {
        if (targetPosition != null)
        {
            if (moving && transform.position == targetPosition.position)
            {
                moving = false;
            }
        }
        
    }

    private void CheckForMove()
    {
        if (enemyObject != null)
        {
            if (enemyObject.GetComponent<AllowMovement>().isFirstToken && firstMove == false)
            {
                enemyObject.GetComponent<AllowMovement>().movementAllowed = true;
                selectedColumn = enemyObject.GetComponent<AllowMovement>().column;
                firstMove = true;
            }
            if (player.GetComponent<PointsTracker>().points == enemyObject.GetComponent<PointsSystem>().pointsRequired && selectedColumn == enemyObject.GetComponent<AllowMovement>().column)
            {
                enemyObject.GetComponent<AllowMovement>().movementAllowed = true;
            }
            else
            {
                enemyObject.GetComponent<AllowMovement>().movementAllowed = false;
            }
        }
    }
}
