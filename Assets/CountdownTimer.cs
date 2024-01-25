using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 5f;
    [SerializeField] Text countdown;
    [SerializeField] CarControls carControls;

    void Start()
    {
        currentTime = startingTime;
        DisableCarControl();
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            countdown.text = currentTime.ToString("0");
        }
        else
        {
            currentTime = 0;
            EnableCarControl();
        }
    }

    void DisableCarControl()
    {
        carControls.enabled = false;
        Debug.Log("Car control disabled.");
    }

    void EnableCarControl()
    {
        carControls.enabled = true;
        Debug.Log("Car control enabled.");
    }

    public bool IsCountdownOver()
    {
        return currentTime <= 0;
    }
}
