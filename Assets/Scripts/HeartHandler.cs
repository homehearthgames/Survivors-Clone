using UnityEngine;

public class HeartHandler : MonoBehaviour
{
    public GameObject backgroundHeartPrefab; // Assign this in the inspector
    public GameObject filledHeartPrefab; // Assign this in the inspector
    private GameObject[] backgroundHearts;
    private GameObject[] filledHearts;
    private PlayerStatsHandler playerStatsHandler;

    public Transform backgroundHeartsObject; // Assign this in the inspector
    public Transform filledHeartsObject; // Assign this in the inspector

    // Use this for initialization
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerStatsHandler = player.GetComponent<PlayerStatsHandler>();
            if (playerStatsHandler != null)
            {
                // Instantiate heart objects for each unit of health.
                backgroundHearts = new GameObject[(int)playerStatsHandler.maxHealth];
                filledHearts = new GameObject[(int)playerStatsHandler.maxHealth];
                for (int i = 0; i < playerStatsHandler.maxHealth; i++)
                {
                    // Instantiate both background and filled hearts
                    backgroundHearts[i] = Instantiate(backgroundHeartPrefab, backgroundHeartsObject);
                    filledHearts[i] = Instantiate(filledHeartPrefab, filledHeartsObject);

                    // Set the position of your hearts properly.
                    backgroundHearts[i].transform.localPosition = new Vector3(i * 1.5f, 0, 0);
                    filledHearts[i].transform.localPosition = new Vector3(i * 1.5f, 0, 0);
                }
            }
            else
            {
                Debug.LogError("PlayerStatsHandler not found on the player GameObject. Please attach PlayerStatsHandler script.");
            }
        }
        else
        {
            Debug.LogError("Player object not found. Please ensure the player object is tagged as 'Player'.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStatsHandler != null)
        {
            // Update hearts based on current health.
            for (int i = 0; i < filledHearts.Length; i++)
            {
                if (i < playerStatsHandler.currentHealth)
                {
                    filledHearts[i].SetActive(true);
                }
                else
                {
                    filledHearts[i].SetActive(false);
                }
            }
        }
    }
}
