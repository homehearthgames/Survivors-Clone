using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private PlayerSpriteAnimator spriteAnimator;
    private PlayerStatsHandler playerStatsHandler;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteAnimator = GetComponent<PlayerSpriteAnimator>();
        playerStatsHandler = GetComponent<PlayerStatsHandler>();

        SetStats();
    }

    private void SetStats()
    {
        speed = playerStatsHandler.speed;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        // Flip the character based on the direction of movement
        if (moveDirection.x != 0) 
        {
            GetComponent<SpriteRenderer>().flipX = moveDirection.x < 0;
        }
    }

void Move()
{
    rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    if(rb.velocity != Vector2.zero)
    {
        spriteAnimator.isMoving = true;
    }
    else
    {
        spriteAnimator.isMoving = false;
    }
}

}
