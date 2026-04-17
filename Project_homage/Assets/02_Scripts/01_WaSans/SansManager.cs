using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SansManager : MonoBehaviour
{
    public static SansManager Instance;
    public TextMeshProUGUI timeText;
    public float gameTime;
    public float currentTime;
    public bool isGame;
    public bool gameOver;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameTime = 50;
        isGame = true;
        gameOver = false;
        currentTime = gameTime;
    }

    void UpdateTimerUI()
    {
        // 시간을 "00:00" 형식으로 변환하여 표시
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Update is called once per frame
    void Update()
    {
        // 게임이 진행중일때 시간이 흐름 (플레이어가 파괴되었을 때 종료)
        if (isGame)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();
            if (currentTime < 0 || gameOver)
            {
                isGame = false;

                if (currentTime < 0) // 시간이 모두 흘렀을 때
                {
                    currentTime = 0;
                    Debug.Log("Game Clear!");
                }
                else if (gameOver) // 플레이어가 파괴되었을 때
                {
                    GameManager.instance.life--;
                    Debug.Log("Game Fail!");
                }

                Invoke(nameof(GameManager.instance.RoundStandby), 2f);
            }
        }
    }
}
