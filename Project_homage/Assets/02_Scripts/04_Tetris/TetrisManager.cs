using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TetrisManager : MonoBehaviour
{
    [Header("Game Settings")]
    public float baseLimitTime = 10f;
    public float speedMultiplier = 1.0f; // 라운드에 따라 외부에서 조절 가능

    [Header("UI Reference")]
    public TextMeshProUGUI timerText;

    private float currentTime;
    public bool isGame = true;
    private int clearedLines = 0;

    void Start()
    {
        currentTime = baseLimitTime;
    }

    void Update()
    {
        if (!isGame) return;

        // 배속 적용: 실제 흐르는 시간보다 multiplier만큼 더 빠르게 감소
        currentTime -= Time.deltaTime * speedMultiplier;
        timerText.text = $"Time: {Mathf.Max(0, currentTime):F2}s";

        if (isGame)
        {
            if (clearedLines >= 4)
            {
                isGame = false;
                Debug.Log("You agreed!");

                Invoke(nameof(Clear), 2f);
            }
            else if (currentTime < 0)
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