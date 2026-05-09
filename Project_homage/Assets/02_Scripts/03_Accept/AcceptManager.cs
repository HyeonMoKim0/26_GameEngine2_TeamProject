using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AcceptManager : MonoBehaviour
{
    public static AcceptManager instance;

    [Header("Game Settings")]
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
        agreeText.SetActive(false);
        disagreeText.SetActive(false);
    }

    public void Agree()
    {
        agree = true;
        agreeText.SetActive(true);
    }

    public void Disagree()
    {
        disagree = true;
        disagreeText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGame)
        {
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
