using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public Transform player; // Assign the player's Transform in the Inspector
    private int lapsCompleted = 0;
    private bool isGamePaused = false;
    private Vector3 lastCheckpoint; // Store the last checkpoint position

    void Start()
    {
        lastCheckpoint = player.position; // Set the starting point as the initial checkpoint
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 playerDirection = player.up; // Get the player's forward direction

            // Assuming the finish line's forward direction is along the track
            Vector2 finishLineDirection = transform.up;

            float dotProduct = Vector2.Dot(playerDirection, finishLineDirection);

            // Check if the dot product is positive (indicating the player is going in the right direction)
            if (dotProduct > 0f)
            {
                lapsCompleted++;

                if (lapsCompleted == 2 && !isGamePaused)
                {
                    PauseGame();
                }
            }
            else
            {
                lapsCompleted--;
                // Player is going in the wrong direction, reset position to the last checkpoint
                Debug.Log("Wrong direction! Resetting position to the last checkpoint.");
                player.position = lastCheckpoint;
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
        isGamePaused = true;

        // Display a message or perform other actions when the game is paused
        Debug.Log("Game Paused. Player crossed the finish line twice.");
    }

    // You can call this method from other scripts to set a new checkpoint
    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpoint = checkpointPosition;
    }
}
