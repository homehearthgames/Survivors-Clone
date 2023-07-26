using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10.0f;
    public float fireRate = 1.0f; // projectiles per second
    private NearestEnemyTracker enemyTracker;
    private float nextFireTime = 0.0f;

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
        projectile.shooter = Projectile.Shooter.Player;
        // Rotate the projectile to face the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectileInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        Rigidbody2D rb = projectileInstance.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;
    }
}
