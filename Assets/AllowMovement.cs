using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowMovement : MonoBehaviour
{
    private GameObject Player;
    public bool movementAllowed;
    public bool isFirstToken;
    public string column;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCheck();
        UpdateColumn();
    }

    private void PlayerCheck()
    {
        if (Player.GetComponent<PointsTracker>().points == this.GetComponent<PointsSystem>().pointsRequired)
        {
            movementAllowed = true;
        }
    }
    private void UpdateColumn()
    {
        column = gameObject.GetComponentInParent<ColumnTracker>().columnName;
    }


}
