using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    [SerializeField] GameObject[] trapPrefab;
    [SerializeField] float minTras;
    [SerializeField] float maxTras;
    private float spawnProbability = 1f;

    [SerializeField] Transform parentTransform;

    // List to keep track of spawned traps
    private List<GameObject> spawnedTraps = new List<GameObject>();

    void Start()
    {
        TrySpawnTrap();
    }

    public void TrySpawnTrap()
    {
        // Destroy previously spawned traps
        foreach (GameObject trap in spawnedTraps)
        {
            Destroy(trap);
        }
        spawnedTraps.Clear(); // Clear the list of spawned traps

        if (Random.value <= spawnProbability)
        {
            SpawnTrap();
        }
    }

    void SpawnTrap()
    {
        if (trapPrefab.Length == 0)
        {
            Debug.LogError("No trap prefab assigned!");
            return;
        }

        if (parentTransform != null)
        {
            float xPosition = Random.Range(minTras, maxTras);
            Vector3 spawnPosition = new Vector3(xPosition, 0f, 0f);

            GameObject newTrap = Instantiate(trapPrefab[Random.Range(0, trapPrefab.Length)]);
            newTrap.transform.SetParent(parentTransform, false);
            newTrap.transform.localPosition = spawnPosition;

            // Add the spawned trap to the list
            spawnedTraps.Add(newTrap);
        }
        else
        {
            Debug.LogError("Parent transform is not assigned!");
        }
    }

    // Method to update the spawn probability
    public void UpdateSpawnProbability(float incrementAmount)
    {
        spawnProbability += incrementAmount;

        // Log the current value of spawnProbability to the console
        Debug.Log("Spawn Probability: " + spawnProbability);
    }
}
