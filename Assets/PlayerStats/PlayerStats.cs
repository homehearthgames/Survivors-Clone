using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private int goldReward;

    [Header("Stats")]
    [SerializeField] private int health;
    [SerializeField] private float speed;
    
    [Header("Melee Settings")]
    [SerializeField] private float attackDamage;

    [Header("Ranged Settings")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private float rangedAttackSpeed;
    [SerializeField] private float attackRange;

    public int GoldReward
    {
        get { return goldReward; }
        set { goldReward = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
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
}
