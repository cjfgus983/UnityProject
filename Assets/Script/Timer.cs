using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    
    [SerializeField] Text timerText; // ø¨∞·«“ UI Text
    private float gameTimer = 0f;

	void Update()
    {
        gameTimer += Time.deltaTime;
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(gameTimer / 60f);
        int seconds = Mathf.FloorToInt(gameTimer % 60f);

        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        timerText.text = "Time: " + timerString;
    }
}
