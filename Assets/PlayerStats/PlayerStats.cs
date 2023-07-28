using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    [Header("Stats")]
    [SerializeField] private int health;

    [Header("Melee Settings")]
    [SerializeField] private float attackDamage;

    [Header("Ranged Settings")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private float rangedAttackSpeed;
    [SerializeField] private float attackRange;

    [Header("New Settings")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float recovery;
    [SerializeField] private float power;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float duration;
    [SerializeField] private float range;
    [SerializeField] private float cooldown;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public float AttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
        set { projectileSpeed = value; }
    }

    public float ProjectileDamage
    {
        get { return projectileDamage; }
        set { projectileDamage = value; }
    }

    public float RangedAttackSpeed
    {
        get { return rangedAttackSpeed; }
        set { rangedAttackSpeed = value; }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    public float Recovery
    {
        get { return recovery; }
        set { recovery = value; }
    }

    public float Power
    {
        get { return power; }
        set { power = value; }
    }

    public float AttackSpeed
    {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }

    public float Duration
    {
        get { return duration; }
        set { duration = value; }
    }

    public float Range
    {
        get { return range; }
        set { range = value; }
    }

    public float Cooldown
    {
        get { return cooldown; }
        set { cooldown = value; }
    }
}
