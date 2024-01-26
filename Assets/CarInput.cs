using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInput : MonoBehaviour
{
    CarControls cCar;
    CountdownTimer countdownTimer;
    AudioSource audioSource;

    public AudioClip keyPressSound;

    private void Start()
    {
        cCar = GetComponent<CarControls>();
        countdownTimer = FindObjectOfType<CountdownTimer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (countdownTimer != null && countdownTimer.IsCountdownOver())
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (Input.GetKeyDown(KeyCode.W))
                PlayKeyPressSound();

            if (Input.GetKeyUp(KeyCode.W))
                StopKeyPressSound();

            cCar.SetInputVector(input);
        }
    }

    private void PlayKeyPressSound()
    {
        if (keyPressSound != null && audioSource != null)
            audioSource.PlayOneShot(keyPressSound);
    }

    private void StopKeyPressSound()
    {
        if (audioSource != null)
            audioSource.Stop();
    }
}
