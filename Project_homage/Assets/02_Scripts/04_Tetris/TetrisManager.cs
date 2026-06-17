using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TetrisManager : MonoBehaviour
{
    public static TetrisManager instance;

    [Header("Game Settings")]
    public float gameTime = 10f;
    private float currentTime;
    public int clearedLines = 0;
    public bool isGame;

    [Header("UI Reference")]
    public Slider timer;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        gameTime = 10f;
        currentTime = gameTime;

        isGame = true;

        BoardData.InitializeRandomLines();
        FindObjectOfType<SpawnBrick>().SpawnTetrisBrick();
    }

    void Update()
    {
        if (isGame)
        {
            // 4줄을 지웠을 때 [Clear]
            if (clearedLines >= 4)
            {
                isGame = false;
                Debug.Log("Tetris!!");

                StartCoroutine(Clear());
            }

            // 제한 시간이 0이 되었을 때 [Fail]
            if (currentTime < 0)
            {
                isGame = false;
                Debug.Log("Time Over!!");

                StartCoroutine(Fail());
            }
        }

        // 타이머 업데이트
        if (isGame)
        {
            currentTime -= Time.deltaTime * GameManager.instance.gameSpeed;
        }
        timer.value = currentTime / gameTime;
    }

    IEnumerator Clear()
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 0f;
        GameManager.instance.RoundOn = false;
        GameManager.instance.RoundStandby();
    }

    IEnumerator Fail()
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 0f;
        GameManager.instance.RoundOn = false;
        GameManager.instance.failedGame();
    }
}