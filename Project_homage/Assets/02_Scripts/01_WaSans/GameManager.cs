using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI timeText;
    public bool gameOver;
    public float gameTime;
    public float currentTime;
    public bool gameClear;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameTime = 50;
        gameOver = false;
        gameClear = false;
        currentTime = gameTime;
    }

    void UpdateTimerUI()
    {
        // 시간을 "00:00" 형식으로 변환하여 표시
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    void SansClear()
    {
        gameClear = true;
        Debug.Log("Clear!");
    }

    void GameOver()
    {
        Debug.Log("Game Over..");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0 && !gameOver)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            currentTime = 0;
            if (!gameOver)
            {
                SansClear();
            }
            else
            {
                GameOver();
            }
        }

    }
}
