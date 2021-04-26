using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private float timeToCompleteLevel;

    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private GameObject winGameScreen;

    [SerializeField]
    private GameObject inGameHud;

    [SerializeField]
    private GameObject startingScreen;

    [SerializeField]
    private GameObject killTarget;

    private bool countingDown;

    public int RemainingSeconds => Mathf.RoundToInt(timeToCompleteLevel);

    void Start()
    {
        Time.timeScale = 1f;
        gameOverScreen.SetActive(false);
        winGameScreen.SetActive(false);
        inGameHud.SetActive(false);
        startingScreen.SetActive(true);
        countingDown = false;
    }

    void Update()
    {
        if(countingDown)
        {
            timeToCompleteLevel -= Time.deltaTime;
            if(timeToCompleteLevel <= 0f)
            {
                GameOver();
            }
            if(killTarget == null)
            {
                WinGame();
            }
        }
    }

    public void StartGame()
    {
        countingDown = true;
        inGameHud.SetActive(true);
        startingScreen.SetActive(false);
    }

    public void GameOver()
    {
        EndGame();
        gameOverScreen.SetActive(true);
    }

    public void WinGame()
    {
        EndGame();
        winGameScreen.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void EndGame()
    {
        countingDown = false;
        Time.timeScale = .1f;
        inGameHud.SetActive(false);
    }
}
