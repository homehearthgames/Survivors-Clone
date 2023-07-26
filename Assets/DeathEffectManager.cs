using System.Collections.Generic;
using UnityEngine;

public class DeathEffectManager : MonoBehaviour
{
    public List<GameObject> deathPrefabs;  // A list of death prefabs you want to randomly choose from

    public void SpawnDeathPrefabs()
    {
        // Check if there is at least one prefab to instantiate
        if(deathPrefabs != null && deathPrefabs.Count > 0)
        {
            // Generate a random index to choose a death prefab from the list
            int randomIndex = Random.Range(0, deathPrefabs.Count);

            // Instantiate the death prefab at this object's position and rotation
            Instantiate(deathPrefabs[randomIndex], transform.position, transform.rotation);
        }
        else
        {
            Debug.LogWarning("DeathEffectManager: No death prefabs assigned.");
        }
    }
}
