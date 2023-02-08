using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    private Vector2 leftBoundry;
    private Vector2 rightBoundry;
    public float speed;
    public bool pastEdge;
    void Start()
    {
        leftBoundry = new Vector2(-500, this.transform.position.y + Random.Range(-10,10));
        rightBoundry = new Vector2(500, this.transform.position.y + Random.Range(-10, 10));
        speed = Random.Range(1f, 1.25f);
    }

    // Update is called once per frame
    void Update()
    {
        BorderCheck();
        MoveCloudsLeft();
        MoveCloudsRight();
       
    }
    private void MoveCloudsLeft()
    {
        if (pastEdge == false)
        {
            speed = Random.Range(1, 1.25f);
            transform.position = Vector2.MoveTowards(this.transform.position, leftBoundry, speed * Time.deltaTime);
        }
    }
    private void BorderCheck()
    {
        if (transform.position.x < -25)
        {
            pastEdge = true;
        }
        if (transform.position.x > 25)
        {
            pastEdge = false; 
        }
    }
    private void MoveCloudsRight()
    {
        if (pastEdge == true)
        {
            speed = Random.Range(1, 1.25f);
            transform.position = Vector2.MoveTowards(this.transform.position, rightBoundry, speed * Time.deltaTime);
        }
    }
}
