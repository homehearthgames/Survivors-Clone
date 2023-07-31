using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum Shooter { Player, Enemy }

    public Shooter shooter;
    public bool piercing; //added piercing property

    [SerializeField] GameObject damageTextPrefab;
    public float damageAmount = 1;

    private void Start() 
    {
        if (!piercing) 
        {
            Destroy(gameObject, 10f); //destroys after 10 seconds if not piercing
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (shooter)
        {
            case Shooter.Player:
                // If the player shot the projectile, look for enemy to damage
                if (other.gameObject.CompareTag("Enemy"))
                {
                    EnemyStatsHandler enemyStatsHandler = other.GetComponent<EnemyStatsHandler>();
                    DealDamage(enemyStatsHandler);
                }
                break;
            case Shooter.Enemy:
                // If the enemy shot the projectile, look for player to damage
                if (other.gameObject.CompareTag("Player"))
                {
                    PlayerStatsHandler playerStatsHandler = other.GetComponent<PlayerStatsHandler>();
                    DealDamage(playerStatsHandler);
                }
                break;
        }
    }

    private void DealDamage(object target)
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
        
        // If the projectile is not piercing, destroy it after it has dealt damage.
        if (!piercing) 
        {
            Destroy(this.gameObject);
        }
    }
}
