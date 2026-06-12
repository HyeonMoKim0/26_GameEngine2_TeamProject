using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AcceptManager : MonoBehaviour
{
    public static AcceptManager instance;

    [Header("Game Settings")]
    public float currentTime = 1f;
    public bool isGame;
    public bool agree;
    public bool disagree;

    [Header("UI Settings")]
    public GameObject agreeText;
    public GameObject disagreeText;

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
            // 동의 버튼을 눌렀을 때 [Clear]
            if (agree)
            {
                isGame = false;
                Debug.Log("You agreed!");

                Invoke(nameof(Clear), 2f);
            }
            
            // 비동의 버튼을 눌렀을 때 [Fail]
            if (disagree)
            {
                isGame = false;
                Debug.Log("You disagreed!");

                Invoke(nameof(Fail), 2f);
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

    void Clear()
    {
        GameManager.instance.RoundStandby();
    }

    void Fail()
    {
        GameManager.instance.failedGame();
    }
}
