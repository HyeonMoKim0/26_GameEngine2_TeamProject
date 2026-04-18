using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptManager : MonoBehaviour
{
    public static AcceptManager instance;

    public bool isGame;
    public bool agree;
    public bool disagree;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isGame = true;
    }

    public void Agree()
    {
        agree = true;
    }

    public void Disagree()
    {
        disagree = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGame)
        {
            if (agree)
            {
                isGame = false;
                Debug.Log("You agreed!");

                Invoke(nameof(Clear), 2f);
            }
            else if (disagree)
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
