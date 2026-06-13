using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmVFX : MonoBehaviour
{
    public static RhythmVFX Instance;

    public GameObject goodVFX;
    public GameObject clearVFX;

    void Awake()
    {
        Instance = this;
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
