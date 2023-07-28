using System;
using System.Collections;
using UnityEngine;

public class PlayerStatsHandler : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    // Create public variables for speed, current health, and max health.
    public float moveSpeed;
    public float currentHealth;
    public float maxHealth;
    public float recovery;
    public float power;
    public float attackSpeed;
    public float duration;
    public float range;
    public float cooldown;

    public PlayerStats playerStats;
    
    [Header("Level Stats")]
    [SerializeField] float currentLevel = 1f;
    [SerializeField] public float currentExperience = 0f;
    [SerializeField] public float requiredExperience = 150f;
    
    // Use this for initialization
    void Awake()
    {
        moveSpeed = playerStats.MoveSpeed;
        maxHealth = playerStats.Health;
        currentHealth = maxHealth;
        recovery = playerStats.Recovery;
        power = playerStats.Power;
        attackSpeed = playerStats.AttackSpeed;
        duration = playerStats.Duration;
        range = playerStats.Range;
        cooldown = playerStats.Cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        CheckExperience();
    }

    private void CheckExperience()
    {
        if (currentExperience >= requiredExperience)
        {
            currentLevel++;
            currentExperience = 0f;
            requiredExperience *= 1.25f;
        }
    }

public void TakeDamage(float damage)
{
    Debug.Log("test");
    currentHealth -= damage;

    // Add these lines to activate the camera shake
    var noise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    noise.m_AmplitudeGain = 2f; // Adjust this value to control the shake amount
    StartCoroutine(StopShake(0.3f)); // Adjust this value to control the shake duration

    if (currentHealth <= 0)
    {
        currentHealth = 0;
    }

}// Add this coroutine method
IEnumerator StopShake(float delay)
{
    yield return new WaitForSeconds(delay);

    var noise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    noise.m_AmplitudeGain = 0f;
}
    // Call this function to heal the player.
    public void Heal(float healing)
    {
        currentHealth += healing;

        // Make sure health doesn't go over max.
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // Call this function to change the player speed.
    public void ChangeSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}
