using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] PlayerStatsHandler playerStatsHandler;
    [SerializeField] Image experienceBarImage;

    // Use this for initialization
    void Start()
    {
        if (playerStatsHandler == null)
        {
            Debug.LogError("PlayerStatsHandler is not set!");
        }

        if (experienceBarImage == null)
        {
            Debug.LogError("Experience bar Image is not set!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateExperienceBar();
    }

    private void UpdateExperienceBar()
    {
        float experienceRatio = playerStatsHandler.currentExperience / playerStatsHandler.requiredExperience;
        experienceBarImage.fillAmount = experienceRatio;
    }
}
