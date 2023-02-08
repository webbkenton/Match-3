using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeTracker : MonoBehaviour
{
    public int Lives;
    public GameObject TokenHolder1;
    public GameObject TokenHolder2;
    public GameObject TokenHolder3;
    public GameObject TokenHolderUIBox;

    private void Start()
    {
        Lives = 3;
    }
    private void Update()
    {
        UpdateLives();
    }

    public void UpdateLives()
    {
        if (Lives == 3)
        {
            TokenHolder1.SetActive(true);
            TokenHolder2.SetActive(true);
            TokenHolder3.SetActive(true);
        }
        if (Lives == 2)
        {
            TokenHolder1.SetActive(false);
            TokenHolder2.SetActive(true);
            TokenHolder3.SetActive(true);
        }
        if (Lives == 1)
        {
            TokenHolder1.SetActive(false);
            TokenHolder2.SetActive(false);
            TokenHolder3.SetActive(true);
        }
        if (Lives == 0)
        {
            TokenHolder1.SetActive(false);
            TokenHolder2.SetActive(false);
            TokenHolder3.SetActive(false);
            Debug.Log("0 Lives Remaining");
            //Ideally We Want the Game Over Screen To Appear at this point.

        }
    }
}
