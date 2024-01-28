using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LapTimer : MonoBehaviour
{
    public FinishLine finishLine;
    public TextMeshProUGUI countdownText;
    public Image MenuBackground;
    public Image gameOverUI;
    public Button gameOverRestartButton;
    public Button gameOverQuitButton;

    private float initialCountdownDuration = 80f;
    private float countdownDuration = 80f;
    private bool isTimerRunning = false;

    [SerializeField] CarControls carControls;

    private void Start()
    {
        MenuBackground.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);
        gameOverRestartButton.gameObject.SetActive(false);
        gameOverQuitButton.gameObject.SetActive(false);

        countdownText.text = FormatTime(initialCountdownDuration);
        StartCoroutine(StartDelay());

        gameOverRestartButton.onClick.AddListener(RestartGame);
        gameOverQuitButton.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            if (countdownDuration > 0f)
            {
                countdownDuration -= Time.deltaTime;
                countdownText.text = FormatTime(countdownDuration);
            }
            else
            {
                countdownDuration = 0f;
                EndGame();
            }
        }
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(5f);
        isTimerRunning = true;
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void EndGame()
    {
        carControls.enabled = false;
        
        MenuBackground.gameObject.SetActive(true);
        gameOverUI.gameObject.SetActive(true);
        gameOverRestartButton.gameObject.SetActive(true);
        gameOverQuitButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        carControls.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("working");

        MenuBackground.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);
        gameOverRestartButton.gameObject.SetActive(false);
        gameOverQuitButton.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void RestartLapTimer()
    {
        countdownDuration = initialCountdownDuration;
    }
}
