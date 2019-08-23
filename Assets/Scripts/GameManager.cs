using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject spawner;
    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countdownPage;
    public GameObject gamePlayUIPage;
    public GameObject pausedPage;
    public GameObject pauseButton;
    public GameObject playerPrefab;
    public Dropdown dropdown;
    public Text pausedPageScore;
    public Text gameOverPageScore;

    public float enemySpeed;
    public float minWaitTimeBetweenCars;
    public float maxWaitTimeBetweenCars;
    public int scoreMultiplier;
    public bool isGameOver = false;

    private Vector3 playerStartPos = new Vector3(0, -3.57f, 0);
    private int score = 0;

    enum PageState
    {
        None,
        Start,
        Countdown,
        Paused,
        GameOver
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnEnable()
    {
        CountdownText.OnCountdownFinished += OnCountdownFinished;
        DestroyPlayer.OnPlayerDied += OnPlayerDied;
    }

    void OnDisable()
    {
        CountdownText.OnCountdownFinished -= OnCountdownFinished;
        DestroyPlayer.OnPlayerDied -= OnPlayerDied;
    }

    public void Start()
    {
        SetPageState(PageState.Start);
    }

    void OnCountdownFinished()
    {
        SetPageState(PageState.None);
    }

    void OnPlayerDied()
    {
        gameOverPageScore.text = "Score: " + ScoreCounter.Instance.score.ToString();

        isGameOver = true;

        score = ScoreCounter.Instance.score;

        int savedScore = PlayerPrefs.GetInt("HighScore");
        if (score > savedScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        SetPageState(PageState.GameOver);
    }

    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                isGameOver = false;
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                gamePlayUIPage.SetActive(true);
                pausedPage.SetActive(false);
                spawner.SetActive(true);
                break;
            case PageState.Start:
                isGameOver = true;
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                gamePlayUIPage.SetActive(false);
                pausedPage.SetActive(false);
                spawner.SetActive(false);
                break;
            case PageState.Countdown:
                isGameOver = true;
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(true);
                gamePlayUIPage.SetActive(false);
                pausedPage.SetActive(false);
                spawner.SetActive(false);
                break;
            case PageState.Paused:
                isGameOver = true;
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                gamePlayUIPage.SetActive(false);
                pausedPage.SetActive(true);
                spawner.SetActive(false);
                break;
            case PageState.GameOver:
                isGameOver = true;
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                countdownPage.SetActive(false);
                gamePlayUIPage.SetActive(false);
                pausedPage.SetActive(false);
                spawner.SetActive(false);
                break;
        }
    }

    public void StartButton()
    {
        SetPageState(PageState.Countdown);
        DifficultySettings();
        Time.timeScale = 1;
    }

    public void PauseButton()
    {
        pausedPageScore.text = "Score: " + ScoreCounter.Instance.score.ToString();
        SetPageState(PageState.Paused);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        SetPageState(PageState.None);
    }

    public void ExitButton()
    {
        GameObject[] destroyables;

        destroyables = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject destruct in destroyables)
            DestroyImmediate(destruct);

        DestroyPlayer.Instance.fireAudio.Stop();
        DestroyPlayer.Instance.bgAudio.Play();
        Destroy(DestroyPlayer.Instance.firePrefab);

        TrackController.Instance.trackSpeed = 1;

        playerPrefab.transform.position = playerStartPos;

        SetPageState(PageState.Start);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void DifficultySettings()
    {
        switch(dropdown.value)
        {
            case 0:
                enemySpeed = -5.0f;
                minWaitTimeBetweenCars = 1.0f;
                maxWaitTimeBetweenCars = 2.0f;
                TrackController.Instance.trackSpeed = 1.0f;
                scoreMultiplier = 1;
                break;
            case 1:
                enemySpeed = -6.5f;
                minWaitTimeBetweenCars = 0.75f;
                maxWaitTimeBetweenCars = 1.5f;
                TrackController.Instance.trackSpeed = 2.0f;
                scoreMultiplier = 2;
                break;
            case 2:
                enemySpeed = -8.0f;
                minWaitTimeBetweenCars = 0.5f;
                maxWaitTimeBetweenCars = 1.0f;
                TrackController.Instance.trackSpeed = 3.0f;
                scoreMultiplier = 3;
                break;
        }
    }
}
