using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRadialProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10.0f;
    public float fireRate = 1.0f; // projectiles per second
    public int numberOfProjectiles = 8; // the number of projectiles to be fired in a radial pattern
    private float nextFireTime = 0.0f;

    [SerializeField] float projectileDamage;


    // Start is called before the first frame update
    void Start()
    {
        // Nothing to do in start for this script
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
    projectile.damageAmount = projectileDamage;
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


}
