using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BombManager : MonoBehaviour
{
    public static BombManager instance;

    public GameObject boomImage;

    [Header("Game Settings")]
    float currentTime;
    float bombTime;

    public bool isGame;
    public bool defused;
    public bool wrong;

    public Slider timer;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        bombTime = 5f;
        currentTime = bombTime;

        isGame = true;
        defused = false;
        wrong = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGame)
        {
            currentTime -= Time.deltaTime * GameManager.instance.gameSpeed;
            timer.value = currentTime / bombTime;

            // 폭탄이 해체되었을 때 [Clear]
            if (defused)
            {
                isGame = false;

                Debug.Log("Bomb Defused! Game Clear!");
                BomBSound.instance.PlayDefuseSFX();

                StartCoroutine(Clear());
            }

            // 시간이 다 되어 폭탄이 터졌을 때 [Fail]
            if (currentTime < 0)
            {
                isGame = false;
                currentTime = 0;

                Debug.Log("Time Over! BOOM!!");
                BomBSound.instance.PlayExplodeSFX();
                boomImage.SetActive(true);

                StartCoroutine(Fail());
            }

            if (wrong) // 잘못된 와이어를 눌렀을 때
            {
                isGame = false;

                Debug.Log("Wrong Wire! BOOM!!");
                BomBSound.instance.PlayExplodeSFX();
                boomImage.SetActive(true);

                StartCoroutine(Fail());
            }
        }
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
