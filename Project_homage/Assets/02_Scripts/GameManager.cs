using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject pauseScreen;
    public int lives = 4;
    public int currentRound = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(pauseScreen);
    }

    public void RandomRoundStart()
    {
        currentRound++;

        int randomRound = Random.Range(1, 4);
        switch (randomRound)
        {
            case 1:
                SceneManager.LoadScene("01_WaSans");
                break;
            case 2:
                SceneManager.LoadScene("02_DefuseBomb");
                break;
            case 3:
                SceneManager.LoadScene("03_Accept");
                break;
        }
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0; 
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void failedGame()
    {
        lives--;
        if (lives > 0)
        {
            RandomRoundStart();
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}
