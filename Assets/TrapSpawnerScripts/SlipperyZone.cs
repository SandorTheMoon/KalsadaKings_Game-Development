using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyZone : MonoBehaviour
{
    public float slipperyDuration = 5f;
    public float slipperyDriftFactor = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplySlipperyEffect(other.gameObject);
            StartCoroutine(RemoveSlipperyEffect(other.gameObject, slipperyDuration));
        }
    }

    void ApplySlipperyEffect(GameObject car)
    {
        CarControls carControls = car.GetComponent<CarControls>();
        if (carControls != null)
        {
            carControls.SetSlipperyDriftFactor(slipperyDriftFactor);
            Debug.Log("Slippery effect applied.");
        }
    }

    IEnumerator RemoveSlipperyEffect(GameObject car, float duration)
    {
        yield return new WaitForSeconds(duration);

        CarControls carControls = car.GetComponent<CarControls>();
        if (carControls != null)
        {
            carControls.ResetDriftFactor();
            Debug.Log("Slippery effect expired.");
        }
    }
}
