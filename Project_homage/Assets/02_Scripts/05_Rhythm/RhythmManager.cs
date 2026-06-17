using System.Collections;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance;

    [Header("Game Setting")]
    public bool isGame;
    public bool gameOver;
    public bool gameClear;
    public float waitTime = 1f;

    public AudioSource RhythmMusic;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(StartGame), waitTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGame)
        {
            if (gameClear)
            {
                isGame = false;

                RhythmSound.Instance.clearSFX.Play();
                Instantiate(RhythmVFX.Instance.clearVFX, new Vector3(-2.59f, 1, -1), Quaternion.identity);
                StartCoroutine(Clear());
            }

            if (gameOver)
            {
                isGame = false;

                StartCoroutine(Fail());
            }
        }
    }

    void StartGame()
    {
        isGame = true;
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
