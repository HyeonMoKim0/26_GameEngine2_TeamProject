using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool gameOver;
    public float gameTime = 50;
    public float currentTime;
    public bool gameClear;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        gameClear = false;
        currentTime = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0 && !gameOver)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            gameClear = true;
            Debug.Log("Minigame Clear");
        }

    }
}
