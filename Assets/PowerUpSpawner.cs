using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] powerPrefab;
    [SerializeField] float minTras;
    [SerializeField] float maxTras;
    private float spawnProbability = 1f;

    [SerializeField] Transform parentTransform;

    void Start()
    {
        TrySpawnPower();
    }

    void TrySpawnPower()
    {
        if (Random.value <= spawnProbability)
        {
            SpawnPower();
        }
    }

    void SpawnPower()
    {
        if (powerPrefab.Length == 0)
        {
            Debug.LogError("No trap prefab assigned!");
            return;
        }

        if (parentTransform != null)
        {
            float xPosition = Random.Range(minTras, maxTras);
            Vector3 spawnPosition = new Vector3(xPosition, 0f, 0f);

            GameObject newPower = Instantiate(powerPrefab[Random.Range(0, powerPrefab.Length)]);
            newPower.transform.SetParent(parentTransform, false);
            newPower.transform.localPosition = spawnPosition;
        }
        else
        {
            Debug.LogError("Parent transform is not assigned!");
        }
    }
}
