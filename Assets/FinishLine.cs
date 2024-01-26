// FinishLine.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public Transform player;
    public LapTimer lapTimer; // Reference to the LapTimer script

    private int lapsCompleted = 0;
    private bool isGamePaused = false;
    private Vector3 lastCheckpoint;

    void Start()
    {
        lastCheckpoint = player.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 playerDirection = player.up;

            Vector2 finishLineDirection = transform.up;

            float dotProduct = Vector2.Dot(playerDirection, finishLineDirection);

            if (dotProduct > 0f)
            {
                lapsCompleted++;

                if (lapsCompleted == 2 && !isGamePaused)
                {
                    PauseGame();
                    lapTimer.StartLapTimer(); // Start the lap timer when the player completes 2 laps
                }
            }
            else
            {
                lapsCompleted--;
                Debug.Log("Wrong direction! Resetting position to the last checkpoint.");
                player.position = lastCheckpoint;
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        isGamePaused = true;

        Debug.Log("Game Paused. Player crossed the finish line twice.");
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpoint = checkpointPosition;
    }
}
