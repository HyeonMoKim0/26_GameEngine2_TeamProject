using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptSound : MonoBehaviour
{
    public static AcceptSound instance;

    public AudioSource agreeSFX;
    public AudioSource disagreeSFX;

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
