using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject pauseScreen;
    public GameObject optionScreen;

    [Header("Ready UI")]
    public GameObject readyScreen;
    public TextMeshProUGUI readyLifeUI;
    public TextMeshProUGUI readyRoundUI;
    public GameObject[] howToPlaies;

    [Header("Main Setting")]
    public int life = 0;
    public int totalRound = 0;
    public float gameSpeed = 1f;

    public bool RoundOn = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(readyScreen);
    }

    public void StartGame()
    {
        life = 4;
        totalRound = 0;
        gameSpeed = 1f;
        RoundStandby();
    }

    public void RoundStandby()
    {
        totalRound++;
        gameSpeed = 1f + (totalRound / 5) * 0.05f;
        ReloadUI();

        int randomRound = UnityEngine.Random.Range(1, 7); // 1~6
        readyScreen.SetActive(true);
        StartCoroutine(LoadScene(randomRound));
    }

    IEnumerator LoadScene(int randomRound)
    {
        HowTo(randomRound);
        Pause.Instance.ResumeGame();
        Pause.Instance.currentGame = randomRound;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(5f);

        Time.timeScale = 1f;
        RoundOn = true;
        HowToOff();
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
                SceneManager.LoadScene("03_Agree");
                break;
            case 4:
                SceneManager.LoadScene("04_Tetris");
                break;
            case 5:
                SceneManager.LoadScene("05_Rhythm");
                break;
            case 6:
                SceneManager.LoadScene("06_Fruit");
                break;
        }
    }

    void HowTo(int randomRound)
    {
        switch (randomRound)
        {
            case 1:
            case 4:
                howToPlaies[0].SetActive(false);
                howToPlaies[1].SetActive(true);
                howToPlaies[2].SetActive(false);
                break;
            case 2:
            case 3:
            case 6:
                howToPlaies[0].SetActive(true);
                howToPlaies[1].SetActive(false);
                howToPlaies[2].SetActive(false);
                break;
            case 5:
                howToPlaies[0].SetActive(false);
                howToPlaies[1].SetActive(false);
                howToPlaies[2].SetActive(true);
                break;
        }
    }

    void HowToOff()
    {
        foreach (GameObject howTo in howToPlaies)
        {
            howTo.SetActive(false);
        }
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
            Time.timeScale = 1f;
            GameOver();
        }
    }

    private void ReloadUI()
    {
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
        if (RoundOn && Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeSelf)
            {
                Pause.Instance.ResumeGame();
            }
            else
            {
                Pause.Instance.PauseGame();
            }
        }
    }
}
