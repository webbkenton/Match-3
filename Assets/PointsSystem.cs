using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSystem : MonoBehaviour
{
    public float pointsRequired;
    private float points;
    private GameObject Player;

    private void Start()
    {
        points = 1;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PlayerAward()
    {
        Player.GetComponent<PointsTracker>().points++;
    }
}
