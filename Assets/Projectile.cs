using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum Shooter { Player, Enemy }

    public Shooter shooter;

    [SerializeField] GameObject damageTextPrefab;
    public float damageAmount = 1;

    private void Start() 
    {
        // Destroy(gameObject, 20f);
    }

    // This function is called when a collision is detected
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (shooter)
        {
            case Shooter.Player:
                // If the player shot the projectile, look for enemy to damage
                if (other.gameObject.CompareTag("Enemy"))
                {
                    EnemyStatsHandler enemyStatsHandler = other.GetComponent<EnemyStatsHandler>();
                    DamageTarget(enemyStatsHandler);
                }
                break;
            case Shooter.Enemy:
                // If the enemy shot the projectile, look for player to damage
                if (other.gameObject.CompareTag("Player"))
                {
                    PlayerStatsHandler playerStatsHandler = other.GetComponent<PlayerStatsHandler>();
                    DamageTarget(playerStatsHandler);
                }
                break;
        }
    }

    private void DamageTarget(object target)
    {
        if (target is PlayerStatsHandler playerStatsHandler)
        {
            playerStatsHandler.TakeDamage(damageAmount);
        }
        else if (target is EnemyStatsHandler enemyStatsHandler)
        {
            enemyStatsHandler.TakeDamage(damageAmount);
        }

        var damageText = Instantiate(damageTextPrefab, transform.position + Vector3.up, Quaternion.identity);
        var textMesh = damageText.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        if (textMesh != null)
        {
            textMesh[0].text = damageAmount.ToString();
        }
        else
        {
            Debug.Log("No TextMeshPro component found");
        }
        // Destroy the projectile itself
        Destroy(this.gameObject);
    }
}
