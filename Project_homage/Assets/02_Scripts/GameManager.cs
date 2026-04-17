using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject pauseScreen;
    public TextMeshProUGUI lifeUI;
    public TextMeshProUGUI roundUI;

    public int lives = 4;
    public int totalRound = 0;

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

    public void RoundStandby()
    {
        totalRound++;
        ReloadUI();

        int randomRound = Random.Range(1, 4);
        SceneManager.LoadScene("10_StandbyScene");
        StartCoroutine(LoadScene(randomRound));
    }

    IEnumerator LoadScene(int randomRound)
    {

        yield return new WaitForSeconds(5f);
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

    IEnumerator HowTo(int randomRound)
    {
        yield return new WaitForSeconds(3f);


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
            RoundStandby();
        }
        else
        {
            GameOver();
        }
    }

    private void ReloadUI()
    {
        lifeUI.text = "Lives: " + lives;
        roundUI.text = "Round: " + totalRound;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    // Start is called before the first frame update
    void Start()
    {
        ReloadUI();
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
