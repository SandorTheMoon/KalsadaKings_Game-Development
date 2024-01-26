using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimer : MonoBehaviour
{
    public FinishLine finishLine;
    public Text countdownText;
    private float initialCountdownDuration = 80f; // Initial countdown duration
    private float countdownDuration = 80f; // Current countdown duration
    private bool isTimerRunning = false;

    private void Start()
    {
        countdownText.text = FormatTime(initialCountdownDuration);
        StartCoroutine(StartDelay());
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            if (countdownDuration > 0f)
            {
                countdownDuration -= Time.deltaTime;
                countdownText.text = FormatTime(countdownDuration);
            }
            else
            {
                countdownDuration = 0f;
                // Handle when the countdown reaches zero (e.g., end the game)
            }
        }
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(5f);
        isTimerRunning = true;
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Call this method when the car crosses the finish line
    public void StartLapTimer()
    {
        countdownDuration = initialCountdownDuration; // Reset the countdown duration to the initial value
    }
}
