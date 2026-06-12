using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisSound : MonoBehaviour
{
    public static TetrisSound instance;

    public AudioSource placementSound;
    public AudioSource completeSound;

    private void Awake()
    {
       instance = this;
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
