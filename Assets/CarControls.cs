// CarControls.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControls : MonoBehaviour
{
    public float driftFactor = 0.97f;
    public float accelerationFactor = 30f;
    public float turnFactor = .8f;
    public float maximumSpeed = 40f;

    float acceleration = 0f;
    float steering = 0f;
    float rotation = 0f;
    float upwardVelocity = 0f;

    Rigidbody2D carRB;

    bool isSpeedBoostActive = false;
    bool isSlipperyEffectActive = false;
    float originalDriftFactor;

    void Awake()
    {
        carRB = GetComponent<Rigidbody2D>();
        originalDriftFactor = driftFactor;
    }

    void FixedUpdate()
    {
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
    }

    void ApplyEngineForce()
    {
        upwardVelocity = Vector2.Dot(transform.up, carRB.velocity);

        if (upwardVelocity > maximumSpeed && acceleration > 0)
        {
            return;
        }

        if (carRB.velocity.sqrMagnitude > maximumSpeed * maximumSpeed && acceleration > 0)
        {
            return;
        }

        Vector2 engineForceVector = transform.up * acceleration * accelerationFactor;
        carRB.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        float lowestSpeedForTurning = Mathf.Max(carRB.velocity.magnitude / 8, 1);
        rotation -= steering * turnFactor * lowestSpeedForTurning;
        carRB.MoveRotation(rotation);
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRB.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRB.velocity, transform.right);

        carRB.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 input)
    {
        steering = input.x;
        acceleration = input.y;
    }

    public void ApplySpeedBoost(float boostAmount)
    {
        if (!isSpeedBoostActive)
        {
            StartCoroutine(ActivateSpeedBoost(boostAmount));
        }
    }

    IEnumerator ActivateSpeedBoost(float boostAmount)
    {
        isSpeedBoostActive = true;
        maximumSpeed += boostAmount;
        accelerationFactor *= 1.5f;

        Debug.Log("Speed boost applied: " + boostAmount);

        yield return new WaitForSeconds(10f);

        maximumSpeed -= boostAmount;
        accelerationFactor /= 1.5f;

        Debug.Log("Speed boost expired");

        isSpeedBoostActive = false;
    }

    public void SetSlipperyDriftFactor(float slipperyDriftFactor)
    {
        if (!isSlipperyEffectActive)
        {
            isSlipperyEffectActive = true;
            driftFactor = slipperyDriftFactor;
            Debug.Log("Slippery effect applied.");
        }
    }

    public void ResetDriftFactor()
    {
        driftFactor = originalDriftFactor;
        isSlipperyEffectActive = false;
        Debug.Log("Slippery effect expired.");
    }
}
