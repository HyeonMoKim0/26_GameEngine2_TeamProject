using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TetrisManager : MonoBehaviour
{
    [Header("Game Settings")]
    public float gameTime = 10f;
    private float currentTime;
    private int clearedLines = 0;
    public bool isGame = true;

    [Header("UI Reference")]
    public TextMeshProUGUI timerText;


    void Start()
    {
        gameTime = 10f;
        currentTime = gameTime;
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

                Invoke(nameof(Clear), 2f);
            }

            // 제한 시간이 0이 되었을 때 [Fail]
            if (currentTime < 0)
            {
                isGame = false;
                Debug.Log("Time Over!!");

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