using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    float meleeDamage;
    private void Awake() {
        meleeDamage = GetComponent<EnemyStatsHandler>().meleeDamage;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the tag 'player'
        if (collision.gameObject.tag == "Player")
        {
            PlayerStatsHandler playerStatsHandler = collision.gameObject.GetComponent<PlayerStatsHandler>();
            playerStatsHandler.TakeDamage(meleeDamage);
            // If it does, then we deactivate the enemy object.
            this.gameObject.SetActive(false);
        }
    }
}
