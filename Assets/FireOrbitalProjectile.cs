using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOrbitalProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public int maxOrbitalProjectiles;
    public float fireRate = 1.0f;
    public float orbitRadius = 5.0f;
    public float orbitSpeed = 10f;
    private float nextFireTime = 0.0f;
    private List<GameObject> projectiles = new List<GameObject>();
    private List<float> angles = new List<float>();
    private bool recalculateAngles = false;

    void Update()
    {
        if (Time.time > nextFireTime && projectiles.Count < maxOrbitalProjectiles)
        {
            FireOrbital();
            nextFireTime = Time.time + 1.0f / fireRate;
        }

        if (recalculateAngles)
        {
            float angleIncrement = 2 * Mathf.PI / projectiles.Count;
            for (int i = 0; i < projectiles.Count; i++)
            {
                angles[i] = angleIncrement * i;
            }
            recalculateAngles = false;
        }

        for (int i = 0; i < projectiles.Count; i++)
        {
            if (projectiles[i] == null)
            {
                projectiles.RemoveAt(i);
                angles.RemoveAt(i);
                // Recalculate angles when a projectile is destroyed.
                recalculateAngles = true;
                i--;
                continue;
            }

            angles[i] += Time.deltaTime * orbitSpeed;

            Vector2 newPosition = new Vector2(Mathf.Cos(angles[i]), Mathf.Sin(angles[i])) * orbitRadius;
            projectiles[i].transform.position = transform.position + new Vector3(newPosition.x, newPosition.y, 0);
        }
    }

    void FireOrbital()
    {
        if (projectiles.Count >= maxOrbitalProjectiles)
        {
            return;
        }

        Vector3 spawnPosition = transform.position + transform.up * orbitRadius;
        GameObject projectileInstance = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        Projectile projectile = projectileInstance.GetComponent<Projectile>();
        projectile.shooter = Projectile.Shooter.Player;
        projectiles.Add(projectileInstance);

        angles.Add(0);
        recalculateAngles = true;

        nextFireTime = Time.time + 1.0f / fireRate;
    }
}
