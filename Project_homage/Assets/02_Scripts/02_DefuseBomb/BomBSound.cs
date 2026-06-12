using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BomBSound : MonoBehaviour
{
    public static BomBSound instance;

    [Header("SFX")]
    public AudioSource cutSFX;
    public AudioSource explodeSFX;
    public AudioSource defuseSFX;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayCutSFX()
    {
        cutSFX.Play();
    }

    public void PlayExplodeSFX()
    {
        explodeSFX.Play();
    }

    public void PlayDefuseSFX()
    {
        defuseSFX.Play();
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
