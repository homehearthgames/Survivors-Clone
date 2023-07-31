using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    Basic,
    Radial,
    Orbital,
    Aura,
    AOE,
    Cone,
    Trail,
    Dash
}

[CreateAssetMenu(fileName = "New BasicProjectileData", menuName = "Projectile Data/Basic", order = 52)]
public class BasicProjectileData : ScriptableObject
{
    public ProjectileType projectileType; // The enum you'll be able to set in the inspector
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float fireRate;
    public PlayerStatType scaling;
    public float damageCoefficient;
    public bool autoLock;
    public bool piercing;
}
