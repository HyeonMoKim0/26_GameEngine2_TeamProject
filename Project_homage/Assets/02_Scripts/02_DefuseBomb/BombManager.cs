using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    public static BombManager instance;

    public bool isGame;
    public bool defused;
    public bool wrong;

    float currentTime;
    float bombTime = 5f;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isGame = true;
        defused = false;
        wrong = false;
        currentTime = bombTime;
    }

    // Update is called once per frame
    void Update()
    {
        // 게임이 진행중일때 폭탄이 살아있으면 시간이 흐름 (폭탄이 터지거나 해체되었을 때 종료)
        if (isGame)
        {
            currentTime -= Time.deltaTime;

            if (currentTime < 0 || defused || wrong)
            {
                isGame = false;

                if (defused) // 폭탄이 해체되었을 때
                {
                    Debug.Log("Bomb Defused! Game Clear!");

                    Invoke(nameof(Clear), 2f);
                }
                else if (currentTime < 0) // 시간이 다 되어 폭탄이 터졌을 때
                {
                    currentTime = 0;

                    Debug.Log("Time Over! BOOM!!");

                    Invoke(nameof(Clear), 2f);
                }
                else if (wrong) // 잘못된 와이어를 눌렀을 때
                {
                    Debug.Log("Wrong Wire! BOOM!!");

                    Invoke(nameof(Fail), 2f);
                }
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
