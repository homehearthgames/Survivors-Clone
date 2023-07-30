using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BasicProjectileData", menuName = "Projectile Data/Basic", order = 52)]
public class BasicProjectileData : ScriptableObject
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float fireRate;
    public PlayerStatType scaling;
    public float damageCoefficient;
}
