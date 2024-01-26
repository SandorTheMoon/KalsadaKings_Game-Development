using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public Transform player;
    public LapTimer lapTimer; // Reference to the LapTimer script

    // List of Traps scripts associated with different parentTrapSpawner GameObjects
    public List<Traps> trapsList = new List<Traps>();

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
                // Increment spawnProbability by 0.05 for each Traps script
                foreach (Traps traps in trapsList)
                {
                    if (traps != null)
                    {
                        traps.UpdateSpawnProbability(0.05f);
                        traps.TrySpawnTrap(); // Try to spawn a trap based on the updated probability
                    }
                }

                // Reset the lap timer when the player crosses the finish line
                lapTimer.StartLapTimer();
            }
            else
            {
                // Reduce spawnProbability by 0.05 for each Traps script
                foreach (Traps traps in trapsList)
                {
                    if (traps != null)
                    {
                        traps.UpdateSpawnProbability(-0.05f);
                    }
                }

                Debug.Log("Wrong direction! Resetting position to the last checkpoint.");
                player.position = lastCheckpoint;
            }
        }
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpoint = checkpointPosition;
    }
}
