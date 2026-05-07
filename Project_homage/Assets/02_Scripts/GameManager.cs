using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Main UI")]
    public GameObject pauseScreen;
    public GameObject readyScreen;
    public TextMeshProUGUI pauseLifeUI;
    public TextMeshProUGUI pauseRoundUI;
    public TextMeshProUGUI readyLifeUI;
    public TextMeshProUGUI readyRoundUI;

    [Header("Main Setting")]
    public int life = 4;
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
        DontDestroyOnLoad(readyScreen);
    }

    public void RoundStandby()
    {
        totalRound++;
        ReloadUI();

        int randomRound = Random.Range(1, 4);
        readyScreen.SetActive(true);
        StartCoroutine(LoadScene(randomRound));
    }

    IEnumerator LoadScene(int randomRound)
    {
        yield return new WaitForSeconds(5f);

        readyScreen.SetActive(false);

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
        life--;

        if (life > 0)
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
        pauseLifeUI.text = "Life: " + life;
        pauseRoundUI.text = "Round: " + totalRound;
        readyLifeUI.text = "Life: " + life;
        readyRoundUI.text = "Round: " + totalRound;
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
