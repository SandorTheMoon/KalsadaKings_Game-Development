using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Image menuBackground;
    public Image menuPopUp;
    public Button XButton;
    public Button restartButton;
    public Button homeButton;
    public Button exitButton;

    public AudioSource buttonClickSound;

    private bool isPaused = false;

    void Start()
    {
        menuBackground.gameObject.SetActive(false);
        menuPopUp.gameObject.SetActive(false);
        XButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);

        restartButton.onClick.AddListener(() => StartCoroutine(DelayedButtonClick(RestartGame)));
        homeButton.onClick.AddListener(() => StartCoroutine(DelayedButtonClick(GoToMainMenu)));
        exitButton.onClick.AddListener(() => StartCoroutine(DelayedButtonClick(ExitGame)));

        XButton.onClick.AddListener(() => StartCoroutine(DelayedButtonClick(ResumeGame)));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;

        menuBackground.gameObject.SetActive(true);
        menuPopUp.gameObject.SetActive(true);
        XButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        homeButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;

        menuBackground.gameObject.SetActive(false);
        menuPopUp.gameObject.SetActive(false);
        XButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }

    IEnumerator DelayedButtonClick(System.Action action)
    {
        if (buttonClickSound != null)
        {
            buttonClickSound.Play();
        }

        float currentTimeScale = Time.timeScale;
        Time.timeScale = 1f;

        yield return new WaitForSecondsRealtime(0.5f);

        Time.timeScale = currentTimeScale;

        action.Invoke();
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    void ExitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
