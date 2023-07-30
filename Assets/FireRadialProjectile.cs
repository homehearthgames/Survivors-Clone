using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRadialProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10.0f;
    public float fireRate = 1.0f; // projectiles per second
    public PlayerStatType scaling;
    public float damageCoefficient = 1f;
    public int numberOfProjectiles = 8; // the number of projectiles to be fired in a radial pattern

    private NearestEnemyTracker enemyTracker;
    private PlayerStatsHandler playerStatsHandler;
    private float nextFireTime = 0.0f;
    private float damageAmount = 1f;

    // Start is called before the first frame update
    void Start()
    {
        enemyTracker = GetComponent<NearestEnemyTracker>();
        playerStatsHandler = GetComponent<PlayerStatsHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFireTime)
        {
            FireRadial();
            nextFireTime = Time.time + 1.0f/fireRate; // calculate the next time to fire based on the firing rate
        }
    }

    void FireRadial()
    {
        Projectile projectile = projectilePrefab.GetComponent<Projectile>();
        projectile.shooter = Projectile.Shooter.Player;

        // Calculate and set the damage amount
        damageAmount = 1 * damageCoefficient * GetPlayerStat(playerStatsHandler);
        projectile.damageAmount = damageAmount;

        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for(int i = 0; i <= numberOfProjectiles - 1; i++)
        {
            float projectileDirXposition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * 1f;
            float projectileDirYposition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * 1f;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - new Vector2(transform.position.x, transform.position.y)).normalized;

            float rotZ = Mathf.Atan2(projectileMoveDirection.y, projectileMoveDirection.x) * Mathf.Rad2Deg;

            GameObject tempObj = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0f, 0f, rotZ));
            tempObj.GetComponent<Rigidbody2D>().velocity = 
                new Vector2(projectileMoveDirection.x * projectileSpeed, 
                            projectileMoveDirection.y * projectileSpeed);

            angle += angleStep;
        }
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
