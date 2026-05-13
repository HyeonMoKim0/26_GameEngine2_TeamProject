using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    public static BombManager instance;

    [Header("Game Settings")]
    float currentTime;
    float bombTime;

    public bool isGame;
    public bool defused;
    public bool wrong;

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

            // 폭탄이 해체되었을 때 [Clear]
            if (defused)
            {
                isGame = false;
                Debug.Log("Bomb Defused! Game Clear!");

                Invoke(nameof(Clear), 2f);
            }

            // 시간이 다 되어 폭탄이 터졌을 때 [Fail]
            if (currentTime < 0)
            {
                isGame = false;
                currentTime = 0;

                Debug.Log("Time Over! BOOM!!");

                Invoke(nameof(Fail), 2f);
            }

            if (wrong) // 잘못된 와이어를 눌렀을 때
            {
                isGame = false;
                Debug.Log("Wrong Wire! BOOM!!");

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
