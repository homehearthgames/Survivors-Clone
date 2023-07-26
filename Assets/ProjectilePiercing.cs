using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePiercing : MonoBehaviour
{

    private void Start() {
        Destroy(gameObject, 5f);
    }
    [SerializeField] GameObject damageTextPrefab;
    public float damageAmount = 1;
    // This function is called when a collision is detected
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has the EnemyStatsHandler component
        if (other.gameObject.GetComponent<EnemyStatsHandler>() != null)
        {
            EnemyStatsHandler enemyStatsHandler = other.GetComponent<EnemyStatsHandler>();
            enemyStatsHandler.TakeDamage(damageAmount);
            var damageText = Instantiate(damageTextPrefab, transform.position + Vector3.up, Quaternion.identity);
            var textMesh = damageText.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
            if (textMesh != null)
            {
                textMesh[0].text = damageAmount.ToString();
            }
            else
            {
                Debug.Log("No TextMeshPro component found");
            }
        }
    }
}
