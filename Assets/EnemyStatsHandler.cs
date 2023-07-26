using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsHandler : MonoBehaviour
{ 
    public EnemyStats enemyStats;
    DeathEffectManager deathEffectManager;
    DropHandler dropHandler;

    public float speed;
    public float meleeDamage;
    public float rangedDamage;
    public float currentHealth;
    public float experienceReward;

    // Start is called before the first frame update
    void Awake()
    {
        deathEffectManager = GetComponent<DeathEffectManager>();
        dropHandler = GetComponent<DropHandler>();

        speed = enemyStats.Speed;
        currentHealth = enemyStats.Health;
        experienceReward = enemyStats.ExpReward;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
            PlayerStatsHandler playerStatsHandler = enemyMovement.player.GetComponent<PlayerStatsHandler>();
            playerStatsHandler.currentExperience += experienceReward;
            deathEffectManager.SpawnDeathPrefabs();
            dropHandler.DropItems();
            this.gameObject.SetActive(false);
        
        }
    }

    // You can call this function to damage the player.
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        // Check if player health has dropped below zero.
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            // Player has died. You can handle player death here if you want.
        }
    }

    private void OnDisable() {
    }
}
