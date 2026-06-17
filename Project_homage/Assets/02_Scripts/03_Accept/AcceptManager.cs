using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AcceptManager : MonoBehaviour
{
    public static AcceptManager instance;

    [Header("Game Settings")]
    public float currentTime = 1f;
    public float agreeTime;
    public bool isGame;
    public bool agree;
    public bool disagree;

    [Header("UI Settings")]
    public GameObject agreeText;
    public GameObject disagreeText;
    public Slider timer;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGame)
        {
            currentTime -= Time.deltaTime * GameManager.instance.gameSpeed;
            timer.value = currentTime / agreeTime;

            // 동의 버튼을 눌렀을 때 [Clear]
            if (agree)
            {
                isGame = false;
                Debug.Log("You agreed!");

                StartCoroutine(Clear());
            }
            
            // 비동의 버튼을 눌렀을 때 [Fail]
            if (disagree)
            {
                isGame = false;
                Debug.Log("You disagreed!");

                if (ButtonPattern.instance.isButtonPattern4)
                {
                    ButtonPattern.instance.pattern4Canvas.SetActive(false);
                }

                StartCoroutine(Fail());
            }

            if (currentTime < 0)
            {
                currentTime = 0;
                disagree = true;
                AcceptSound.instance.disagreeSFX.Play();
                disagreeText.SetActive(true);
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
