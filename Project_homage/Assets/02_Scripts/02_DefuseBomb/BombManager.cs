using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    public static BombManager instance;
    public bool bombAlive;
    public bool gameOver;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        bombAlive = true;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
