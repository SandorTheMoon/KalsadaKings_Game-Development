using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public int index;
    public GameObject[] cars;
    public Transform carParent;
    public Vector3 spawnPosition;
    public Vector3 fixedSize = new Vector3(1f, 1f, 1f);

    void Start()
    {
        index = PlayerPrefs.GetInt("CarIndex");
        GameObject car = Instantiate(cars[index], spawnPosition, Quaternion.identity);

        if (carParent != null)
        {
            car.transform.SetParent(carParent, false);
        }

        car.transform.localScale = fixedSize;
        car.transform.localPosition = Vector3.zero;
        car.transform.localRotation = Quaternion.identity;
    }
}
