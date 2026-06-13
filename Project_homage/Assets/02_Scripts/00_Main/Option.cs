using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public GameObject optionScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OptionOn()
    {
        optionScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            optionScreen.SetActive(false);
        }
    }
}
