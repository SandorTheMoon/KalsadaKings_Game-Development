using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostPowerUp : MonoBehaviour
{
    public float speedBoostAmount = 70f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplySpeedBoost(other.gameObject);
            Destroy(gameObject);
        }
    }

    void ApplySpeedBoost(GameObject car)
    {
        CarControls carControls = car.GetComponent<CarControls>();
        if (carControls != null)
        {
            carControls.ApplySpeedBoost(speedBoostAmount);
            Debug.Log("Speed boost applied: " + speedBoostAmount);
        }
    }
}
