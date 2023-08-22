using System.Collections;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    private float countdownTime;
    private bool isOvertime = false;

    private void Start()
    {
        SetInitialCountdownTime();
        StartCoroutine(CountdownRoutine());
    }

    private void SetInitialCountdownTime()
    {
        countdownTime = 25.0f + (GameManager.Instance.currentRound * 5); // Accessing currentRound from GameManager
    }

    IEnumerator CountdownRoutine()
    {
        while (true)
        {
            int minutes = Mathf.FloorToInt(countdownTime / 60F);
            int seconds = Mathf.FloorToInt(countdownTime - minutes * 60);

            if (minutes > 0)
                countdownText.text = string.Format("{0}m {1}s", minutes, seconds);
            else
                countdownText.text = string.Format("{0}s", seconds);

            yield return new WaitForSeconds(1.0f);

            if (!isOvertime)
            {
                countdownTime--;

                if (countdownTime <= 0)
                {
                    EnterOvertimeMode();
                }
            }
            else
            {
                countdownTime++;
            }
        }
    }


    private void EnterOvertimeMode()
    {
        isOvertime = true;
        countdownText.color = Color.red;
    }
}
