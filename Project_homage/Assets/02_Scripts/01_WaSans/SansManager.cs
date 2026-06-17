using System.Collections;
using UnityEngine;

public class SansManager : MonoBehaviour
{
    public static SansManager Instance;

    [Header("Game Setting")]
    public float currentTime;
    public bool isGame;
    public bool gameOver;

    [Header("Sound Setting")]
    public AudioSource bgm;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 1.1f;
        isGame = true;
        gameOver = false;
        bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // 게임이 진행중일때 시간이 흐름 (플레이어가 파괴되었을 때 종료)
        if (isGame)
        {
            currentTime -= Time.deltaTime * GameManager.instance.gameSpeed;

            // 시간이 모두 흘렀을 때 [Clear]
            if (currentTime < 0)
            {
                isGame = false;
                currentTime = 0;
                Debug.Log("Game Clear!");

                StartCoroutine(Clear());
            }

            // 플레이어가 파괴되었을 때 [Fail]
            if (gameOver)
            {
                isGame = false;
                Debug.Log("Game Fail!");

                StartCoroutine(Fail());
            }
        }
    }

    IEnumerator Clear()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
        GameManager.instance.RoundOn = false;

        yield return new WaitForSecondsRealtime(1f);
        bgm.Stop();
        GameManager.instance.RoundStandby();
    }

    IEnumerator Fail()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
        GameManager.instance.RoundOn = false;

        yield return new WaitForSecondsRealtime(1f);
        bgm.Stop();
        GameManager.instance.failedGame();
    }
}
