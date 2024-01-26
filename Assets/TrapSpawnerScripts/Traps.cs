using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject[] trapPrefab;
    [SerializeField] float minTras;
    [SerializeField] float maxTras;
    private float spawnProbability = 0f;

    [SerializeField] Transform parentTransform;

    void Start()
    {
        TrySpawnTrap();
    }

    void TrySpawnTrap()
    {
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
        }
        else
        {
            Debug.LogError("Parent transform is not assigned!");
        }
    }
}
