using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPool; // Array of your enemies
    [SerializeField] private float spawnRate = 5f; // Rate at which enemies will spawn
    [SerializeField] private int maxEnemies = 5; // Maximum enemies to spawn at once
    [SerializeField] private BoxCollider2D spawnArea; // The area within which enemies can spawn
    [SerializeField] private float minDistanceFromPlayer = 5f; // Minimum distance from player

    private bool spawnEnabled = false; // Controls whether spawning is currently enabled
    private GameObject player; // Reference to the player

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player"); // Fetch player object by its tag
        StartSpawning();
    }

    // Method to start spawning enemies
    public void StartSpawning()
    {
        spawnEnabled = true;
        StartCoroutine(SpawnEnemies());
    }

    // Method to stop spawning enemies
    public void StopSpawning()
    {
        spawnEnabled = false;
        StopCoroutine(SpawnEnemies());
    }

    // Coroutine to spawn enemies at set intervals
    private IEnumerator SpawnEnemies()
    {
        while (spawnEnabled)
        {
            for (int i = 0; i < maxEnemies; i++)
            {
                // Spawn the enemy at a random location within the spawner's boundaries
                Vector2 spawnPos = GetSpawnPosition();

                if (spawnPos != Vector2.zero) // If a valid position was found
                {
                    // Choose a random enemy from the enemy pool
                    GameObject chosenEnemy = enemyPool[Random.Range(0, enemyPool.Length)];

                    // Instantiate the chosen enemy
                    Instantiate(chosenEnemy, spawnPos, Quaternion.identity);
                }
            }

            // Wait for spawnRate seconds before spawning again
            yield return new WaitForSeconds(spawnRate);
        }
    }

    // Get a random position within the spawn area but not too close to the player
    private Vector2 GetSpawnPosition()
    {
        for (int i = 0; i < 100; i++) // Try 100 times
        {
            float x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            float y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);

            Vector2 pos = new Vector2(x, y);

            // Check if the position is too close to the player
            if (Vector2.Distance(pos, player.transform.position) > minDistanceFromPlayer)
            {
                // If not, return this position
                return pos;
            }
        }

        // If a valid position was not found after 100 tries, return zero
        return Vector2.zero;
    }
}
