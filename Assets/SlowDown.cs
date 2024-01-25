using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSlowdownTrigger : MonoBehaviour
{
    public static bool isCarInSlowdownZone = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isCarInSlowdownZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isCarInSlowdownZone = false;
        }
    }
}
