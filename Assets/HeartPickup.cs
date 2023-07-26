using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    [SerializeField] private float healingAmount = 1f; // How much to heal when the heart is picked up

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerStatsHandler playerStats = collision.GetComponent<PlayerStatsHandler>();

        if (playerStats != null) // If the collided object has a PlayerStatsHandler
        {
            // Check if the player's health is less than max health before healing
            if (playerStats.currentHealth < playerStats.maxHealth) 
            {
                playerStats.Heal(healingAmount); // Heal the player
                Destroy(gameObject); // Remove the heart pickup after it has been collected
            }
        }
    }
}
