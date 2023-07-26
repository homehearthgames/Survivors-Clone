using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public float countdownTime = 60.0f;

    void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            int minutes = Mathf.FloorToInt(countdownTime / 60F);
            int seconds = Mathf.FloorToInt(countdownTime - minutes * 60);

            if (minutes > 0)
                countdownText.text = string.Format("{0:00}m {1:00}s", minutes, seconds);
            else
                countdownText.text = string.Format("{0:00}s", seconds);

            yield return new WaitForSeconds(1.0f);
            countdownTime--;
        }

        countdownText.text = "Time's Up!";
    }
}
