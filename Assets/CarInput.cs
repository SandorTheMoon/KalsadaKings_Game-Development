using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInput : MonoBehaviour
{
    CarControls cCar;
    CountdownTimer countdownTimer;

    void Start()
    {
        cCar = GetComponent<CarControls>();
        countdownTimer = FindObjectOfType<CountdownTimer>();
    }

    void Update()
    {
        if (countdownTimer != null && countdownTimer.IsCountdownOver())
        {
            Vector2 input = Vector2.zero;
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            cCar.SetInputVector(input);
        }
    }
}
