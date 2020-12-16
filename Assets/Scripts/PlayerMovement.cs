using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 movement;

    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    public Vector2 startingPosition;


    private void Start()
    {
    
        transform.position = startingPosition;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        
        if (movement != Vector2.zero)
        {
            animator.SetFloat("LastX", movement.x);
            animator.SetFloat("LastY", movement.y);
        }
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
