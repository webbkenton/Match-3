using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTokenMover : MonoBehaviour
{
    private Button platform1;
    private Button platform2;
    private Button platform3;
    private Button platform4;
    private Button platform5;
    private Button platform6;
    private Button platform7;
    private Button platform8;
    private Button platform9;
    private Button platform10;

    public GameObject playerToken;
    public MoveCounter moveCounter;
    public Transform eventTransform;

    // Start is called before the first frame update
    void Start()
    {
        moveCounter = GameObject.FindGameObjectWithTag("MoveCounter").GetComponent<MoveCounter>();
        platform1 = GameObject.FindGameObjectWithTag("Platform1").GetComponent<Button>();
        platform2 = GameObject.FindGameObjectWithTag("Platform2").GetComponent<Button>();
        platform3 = GameObject.FindGameObjectWithTag("Platform3").GetComponent<Button>();
        platform4 = GameObject.FindGameObjectWithTag("Platform4").GetComponent<Button>();
        platform5 = GameObject.FindGameObjectWithTag("Platform5").GetComponent<Button>();
        platform6 = GameObject.FindGameObjectWithTag("Platform6").GetComponent<Button>();
        platform7 = GameObject.FindGameObjectWithTag("Platform7").GetComponent<Button>();
        platform8 = GameObject.FindGameObjectWithTag("Platform8").GetComponent<Button>();
        platform9 = GameObject.FindGameObjectWithTag("Platform9").GetComponent<Button>();
        platform10 = GameObject.FindGameObjectWithTag("Platform10").GetComponent<Button>();

        platform1.interactable = true;
        platform2.interactable = false;
        platform3.interactable = false;
        platform4.interactable = false;
        platform5.interactable = false;
        platform6.interactable = false;
        platform7.interactable = false;
        platform8.interactable = false;
        platform9.interactable = false;
        platform10.interactable = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        eventTransform = this.transform;
    }
    private void Update()
    {
        PeiceMoved();
    }

    private void PeiceMoved()
    {
        if (moveCounter.moveCounter == 1)
        {
            platform2.interactable = true;
        }
        if (moveCounter.moveCounter == 2)
        {
            platform3.interactable = true;
        }
        if (moveCounter.moveCounter == 3)
        {
            platform4.interactable = true;
        }
        if (moveCounter.moveCounter == 4)
        {
            platform5.interactable = true;
        }
        if (moveCounter.moveCounter == 5)
        {
            platform6.interactable = true;
        }
        if (moveCounter.moveCounter == 6)
        {
            platform7.interactable = true;
        }
        if (moveCounter.moveCounter == 7)
        {
            platform8.interactable = true;
        }
        if (moveCounter.moveCounter == 8)
        {
            platform9.interactable = true;
        }
        if (moveCounter.moveCounter == 9)
        {
            platform10.interactable = true;
        }

    }


}
