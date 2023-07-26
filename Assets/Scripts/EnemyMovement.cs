using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public GameObject player;
    private SpriteRenderer spriteRenderer;

    private EnemyStatsHandler enemyStats;
    private EnemySpriteAnimator enemySpriteAnimator;  // new reference to the animator

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        enemyStats = GetComponent<EnemyStatsHandler>();
        enemySpriteAnimator = GetComponent<EnemySpriteAnimator>();  // initialize animator

        SetStats();
    }

    private void SetStats()
    {
        speed = enemyStats.speed;
    }

    void Update()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        movement = direction;

        // Check the direction of movement and flip the sprite accordingly
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        // Check whether the enemy is attacking before moving
        if (!enemySpriteAnimator.IsAttacking)
        {
            rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
        }
    }
}
