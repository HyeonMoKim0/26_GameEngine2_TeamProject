using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static Pause Instance;

    public int currentGame = 0;

    [Header("Pause UI")]
    public TextMeshProUGUI pauseLifeUI;
    public TextMeshProUGUI pauseRoundUI;
    public GameObject warningUI;
    public GameObject[] howToPlaies;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        gameObject.SetActive(true);
        HowTo(currentGame);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        if (GameManager.instance.RoundOn) Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void GoToTitle()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("00_Title");
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

    // Update is called once per frame
    void Update()
    {
        pauseLifeUI.text = "남은 목숨 : " + GameManager.instance.life;
        pauseRoundUI.text = "QUEST #" + GameManager.instance.totalRound;

        if (GameManager.instance.totalRound > 4)
        {
            warningUI.SetActive(true);
        }
    }
}
