using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpriteAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] walkingSprites;
    [SerializeField] private Sprite[] attackSprites;
    [SerializeField] private bool canAttack = true;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private bool loop = true;
    [SerializeField] private float speedModifier = 1f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool randomStartingFrame;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    private EnemyStatsHandler enemyStatsHandler;
    private GameObject player;
    private int m_Index = 0;
    private float m_Frame = 0;
    private bool isAttacking = false;

    // Expose the isAttacking variable
    public bool IsAttacking => isAttacking;

    void Awake()
    {
        enemyStatsHandler = GetComponent<EnemyStatsHandler>();
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        m_Frame = 1;

        if (randomStartingFrame)
        {
            m_Index = Random.Range(0, walkingSprites.Length - 1);
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Time.timeScale == 0 || !loop && m_Index == walkingSprites.Length) return;

        // Check distance to player
        if (canAttack && Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            // Switch to attack animation
            m_Frame += Time.deltaTime * speedModifier;
            if (m_Frame >= 1f / 6f)
            {
                spriteRenderer.sprite = attackSprites[m_Index];
                m_Frame = 0;
                m_Index++;
                if (m_Index >= attackSprites.Length)
                {
                    if (loop)
                    {
                        m_Index = 0;
                    }
                    // Fire projectile
                    FireProjectile();
                }
                isAttacking = true;
            }
        }
        else
        {
            // Play walking animation
            m_Frame += Time.deltaTime * speedModifier;
            if (m_Frame >= 1f / 12f)
            {
                spriteRenderer.sprite = walkingSprites[m_Index];
                m_Frame = 0;
                m_Index++;
                if (m_Index >= walkingSprites.Length && loop)
                {
                    m_Index = 0;
                }
                isAttacking = false;
            }
        }
    }

    public void SetIndex(int index)
    {
        m_Index = index;
    }

    public void Draw()
    {
        spriteRenderer.sprite = isAttacking ? attackSprites[m_Index] : walkingSprites[m_Index];
    }

    void FireProjectile()
    {
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.damageAmount = enemyStatsHandler.rangedDamage;
            projectile.shooter = Projectile.Shooter.Enemy;

            // Determine the direction to the player
            Vector2 direction = (player.transform.position - projectileSpawnPoint.position).normalized;

            // Calculate the rotation to face the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Apply the rotation to the projectile
            projectileObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

            // Add a Rigidbody2D if it's not already attached to the projectile
            Rigidbody2D rb = projectileObject.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = projectileObject.AddComponent<Rigidbody2D>();
            }

            // Set the Rigidbody2D to be kinematic so it isn't affected by gravity or other forces
            rb.isKinematic = true;

            // Apply velocity to the projectile to make it move
            float projectileSpeed = 5f; // Change this value as you see fit
            rb.velocity = direction * projectileSpeed;
        }
    }
}
