using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestEnemyTracker : MonoBehaviour
{
    public GameObject nearestEnemy;

    // Update is called once per frame
    void Update()
    {
        nearestEnemy = FindNearestEnemy();
    }

    GameObject FindNearestEnemy()
    {
        EnemyStatsHandler[] enemies;
        enemies = FindObjectsOfType<EnemyStatsHandler>();
        
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (EnemyStatsHandler enemy in enemies)
        {
            GameObject go = enemy.gameObject;
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

}
