using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishLine : MonoBehaviour
{
    public Transform player;
    public LapTimer lapTimer;
    public TextMeshProUGUI levelCountText;

    public List<Traps> trapsList = new List<Traps>();
    public List<PowerUpSpawner> powerUpSpawners = new List<PowerUpSpawner>();

    private Vector3 lastCheckpoint;
    private int currentLevel = 0;

    void Start()
    {
        lastCheckpoint = player.position;
        UpdateLevelCountText();
    }

  void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        Vector3 carFrontLocal = transform.InverseTransformPoint(player.position + player.up * (player.localScale.y / 2.0f));
        Vector3 carBackLocal = transform.InverseTransformPoint(player.position - player.up * (player.localScale.y / 2.0f));

        if (carFrontLocal.y < 0.0f || carBackLocal.y < 0.0f)
        {
            Debug.Log("Checkpoint triggered! CarFrontLocal Y: " + carFrontLocal.y + ", CarBackLocal Y: " + carBackLocal.y);

            foreach (Traps traps in trapsList)
            {
                if (traps != null)
                {
                    traps.UpdateSpawnProbability(0.05f);
                    traps.TrySpawnTrap();
                }
            }

            currentLevel++;
            UpdateLevelCountText();

            lapTimer.RestartLapTimer();

            foreach (PowerUpSpawner powerUpSpawner in powerUpSpawners)
            {
                if (powerUpSpawner != null)
                {
                    powerUpSpawner.TrySpawnPower();
                }
            }
        }
        else
        {
            Debug.Log("Wrong direction or position! CarFrontLocal Y: " + carFrontLocal.y + ", CarBackLocal Y: " + carBackLocal.y);

            foreach (Traps traps in trapsList)
            {
                if (traps != null)
                {
                    traps.UpdateSpawnProbability(-0.05f);
                }
            }

            if (currentLevel > 1)
            {
                currentLevel--;
                UpdateLevelCountText();
            }

            player.position = lastCheckpoint;
        }
    }
}


    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpoint = checkpointPosition;
    }

    private void UpdateLevelCountText()
    {
        if (levelCountText != null)
        {
            levelCountText.text = "Level " + currentLevel.ToString();
        }
    }
}
