using UnityEngine;

public class FireBasicProjectile : MonoBehaviour
{
    public BasicProjectileData[] projectileData;

    private NearestEnemyTracker enemyTracker;
    private Vector2 directionOfMovement;
    private Rigidbody2D rb;
    private float[] nextFireTimes;
    private float damageAmount = 1f;

    // Start is called before the first frame update
    void Start()
    {
        enemyTracker = GetComponent<NearestEnemyTracker>();
        rb = GetComponent<Rigidbody2D>();
        nextFireTimes = new float[projectileData.Length]; // One timer for each projectile data
        directionOfMovement = Vector2.right; // Default initial direction
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 0) // Update direction only when moving
        {
            directionOfMovement = rb.velocity.normalized;
        }

        for (int i = 0; i < projectileData.Length; i++)
        {
            if (Time.time > nextFireTimes[i])
            {
                GameObject target = null;
                Vector2 direction;

                if (projectileData[i].autoLock)
                {
                    target = enemyTracker.nearestEnemy;
                    if (target == null) continue;
                    direction = (target.transform.position - transform.position).normalized;
                }
                else
                {
                    direction = directionOfMovement; // Use last direction even when not moving
                }

                FireAtEnemy(direction, projectileData[i]);
                nextFireTimes[i] = Time.time + 1.0f / projectileData[i].fireRate; // calculate the next time to fire based on the firing rate
            }
        }
    }

    void FireAtEnemy(Vector2 direction, BasicProjectileData data)
    {
        GameObject projectileInstance = Instantiate(data.projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = projectileInstance.GetComponent<Projectile>();
        PlayerStatsHandler playerStatsHandler = GetComponent<PlayerStatsHandler>();

        projectile.shooter = Projectile.Shooter.Player;
        projectile.piercing = data.piercing; //added line to set piercing property
        // Rotate the projectile to face the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectileInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Rigidbody2D rb = projectileInstance.GetComponent<Rigidbody2D>();
        rb.velocity = direction * data.projectileSpeed;

        // Calculate and set the damage amount
        damageAmount = 1 * data.damageCoefficient * GetPlayerStat(playerStatsHandler, data.scaling);
        projectile.damageAmount = damageAmount;
    }

    private float GetPlayerStat(PlayerStatsHandler playerStatsHandler, PlayerStatType scaling)
    {
        return scaling switch
        {
            PlayerStatType.MaxHealth => playerStatsHandler.maxHealth,
            PlayerStatType.MoveSpeed => playerStatsHandler.moveSpeed,
            PlayerStatType.Recovery => playerStatsHandler.recovery,
            PlayerStatType.Power => playerStatsHandler.power,
            PlayerStatType.AttackSpeed => playerStatsHandler.attackSpeed,
            PlayerStatType.Duration => playerStatsHandler.duration,
            PlayerStatType.Range => playerStatsHandler.range,
            PlayerStatType.Cooldown => playerStatsHandler.cooldown,
            _ => 1f,
        };
    }
}
