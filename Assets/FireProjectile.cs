using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStatType 
{
    MaxHealth,
    MoveSpeed,
    Recovery,
    Power,
    AttackSpeed,
    Duration,
    Range,
    Cooldown
}

public class FireProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10.0f;
    public float fireRate = 1.0f; // projectiles per second
    public PlayerStatType scaling;
    public float damageCoefficient = 1f;

    private NearestEnemyTracker enemyTracker;
    private float nextFireTime = 0.0f;
    private float damageAmount = 1f;

    // Start is called before the first frame update
    void Start()
    {
        enemyTracker = GetComponent<NearestEnemyTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTracker.nearestEnemy != null && Time.time > nextFireTime)
        {
            FireAtEnemy(enemyTracker.nearestEnemy);
            nextFireTime = Time.time + 1.0f/fireRate; // calculate the next time to fire based on the firing rate
        }
    }

    void FireAtEnemy(GameObject target)
    {
        GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 direction = (target.transform.position - transform.position).normalized;
        Projectile projectile = projectileInstance.GetComponent<Projectile>();
        PlayerStatsHandler playerStatsHandler = GetComponent<PlayerStatsHandler>();

        projectile.shooter = Projectile.Shooter.Player;
        // Rotate the projectile to face the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectileInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        Rigidbody2D rb = projectileInstance.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;

        // Calculate and set the damage amount
        damageAmount = 1 * damageCoefficient * GetPlayerStat(playerStatsHandler);
        projectile.damageAmount = damageAmount;
    }

    private float GetPlayerStat(PlayerStatsHandler playerStatsHandler)
    {
        switch(scaling)
        {
            case PlayerStatType.MaxHealth: 
                return playerStatsHandler.maxHealth;
            case PlayerStatType.MoveSpeed: 
                return playerStatsHandler.moveSpeed;
            case PlayerStatType.Recovery: 
                return playerStatsHandler.recovery;
            case PlayerStatType.Power: 
                return playerStatsHandler.power;
            case PlayerStatType.AttackSpeed: 
                return playerStatsHandler.attackSpeed;
            case PlayerStatType.Duration: 
                return playerStatsHandler.duration;
            case PlayerStatType.Range: 
                return playerStatsHandler.range;
            case PlayerStatType.Cooldown: 
                return playerStatsHandler.cooldown;
            default:
                return 1f;
        }
    }
}
