using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    public GameObject[] cars;
    public Button prev;
    public Button next;
    int index;

    void Start()
    {
        index = PlayerPrefs.GetInt("CarIndex");

        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(false);
        }

        // Make sure the index is within valid range
        if (index < 0 || index >= cars.Length)
        {
            index = 0; // Set default index if out of range
        }

        cars[index].SetActive(true);
    }

    void Update()
    {
        if (index >= cars.Length - 1)
        {
            next.interactable = false;
        }
        else
        {
            next.interactable = true;
        }

        if (index <= 0)
        {
            prev.interactable = false;
        }
        else
        {
            prev.interactable = true;
        }
    }

    public void Next()
    {
        index = Mathf.Clamp(index + 1, 0, cars.Length - 1);
        UpdateCarSelection();
    }

    public void Prev()
    {
        index = Mathf.Clamp(index - 1, 0, cars.Length - 1);
        UpdateCarSelection();
    }

    void UpdateCarSelection()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(false);
        }

        cars[index].SetActive(true);
        PlayerPrefs.SetInt("CarIndex", index);
        PlayerPrefs.Save();
    }

    public void Race()
    {
        SceneManager.LoadScene("NewScene");
    }
}
