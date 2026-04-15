using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int lives = 4;
    public int currentRound;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RandomRound()
    {
        int randomRound = Random.Range(1, 4);
        switch (randomRound)
        {
            case 1:
                SceneManager.LoadScene("01_Wa Sans");
                break;
            case 2:
                SceneManager.LoadScene("02_Defuse Bomb");
                break;
            case 3:
                SceneManager.LoadScene("03_Accept");
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
