using UnityEngine;

public class FireBasicProjectile : MonoBehaviour
{
    public BasicProjectileData[] projectileData;

    private NearestEnemyTracker enemyTracker;
    private Vector2 directionOfMovement;
    private Rigidbody2D rb;
    private float[] nextFireTimes;
    private float damageAmount = 1f;

    void Start()
    {
        enemyTracker = GetComponent<NearestEnemyTracker>();
        rb = GetComponent<Rigidbody2D>();
        nextFireTimes = new float[projectileData.Length];
        directionOfMovement = Vector2.right;
    }

    void Update()
    {
        if (rb.velocity.magnitude > 0)
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
                    direction = directionOfMovement;
                }

                switch (projectileData[i].projectileType)
                {
                    case ProjectileType.Basic:
                        FireAtEnemy(direction, projectileData[i]);
                        break;
                    case ProjectileType.Radial:
                        FireRadial(projectileData[i]);
                        break;
                }

                nextFireTimes[i] = Time.time + 1.0f / projectileData[i].fireRate;
            }
        }
    }

    void FireAtEnemy(Vector2 direction, BasicProjectileData data)
    {
        GameObject projectileInstance = Instantiate(data.projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = projectileInstance.GetComponent<Projectile>();
        PlayerStatsHandler playerStatsHandler = GetComponent<PlayerStatsHandler>();

        projectile.shooter = Projectile.Shooter.Player;
        projectile.piercing = data.piercing;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectileInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Rigidbody2D rb = projectileInstance.GetComponent<Rigidbody2D>();
        rb.velocity = direction * data.projectileSpeed;

        damageAmount = 1 * data.damageCoefficient * GetPlayerStat(playerStatsHandler, data.scaling);
        projectile.damageAmount = damageAmount;
    }

    void FireRadial(BasicProjectileData data)
    {
        float angleStep = 360f / data.numberOfProjectiles;
        float angle = 0f;
        for (int i = 0; i <= data.numberOfProjectiles - 1; i++)
        {
            float projectileDirXposition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * 1f;
            float projectileDirYposition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * 1f;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - new Vector2(transform.position.x, transform.position.y)).normalized;

            float rotZ = Mathf.Atan2(projectileMoveDirection.y, projectileMoveDirection.x) * Mathf.Rad2Deg;

            GameObject tempObj = Instantiate(data.projectilePrefab, transform.position, Quaternion.Euler(0f, 0f, rotZ));
            tempObj.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDirection.x * data.projectileSpeed,
                            projectileMoveDirection.y * data.projectileSpeed);

            angle += angleStep;
        }
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
