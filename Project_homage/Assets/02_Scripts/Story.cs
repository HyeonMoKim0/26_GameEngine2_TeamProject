using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    private int storyID = 0;

    [Header("STORY")]
    public GameObject[] dialog;
    public GameObject[] image;

    // Start is called before the first frame update
    void Start()
    {
        storyID = 0;
        foreach (GameObject go in image)
        {
            go.SetActive(true);
        }
    }

    public void StorySkip()
    {
        switch (storyID)
        {
            case 0:
                dialog[0].SetActive(false); dialog[1].SetActive(true); break;
            case 1:
                dialog[1].SetActive(false); image[0].SetActive(false); break;
            case 2:
                dialog[2].SetActive(false); dialog[3].SetActive(true); break;
            case 3:
                dialog[3].SetActive(false); image[1].SetActive(false); break;
            case 4:
                dialog[4].SetActive(false); image[2].SetActive(false); break;
            case 5:
                dialog[5].SetActive(false); dialog[6].SetActive(true); break;
            case 6:
                dialog[6].SetActive(false); image[3].SetActive(false); break;
            case 7:
                dialog[7].SetActive(false); dialog[8].SetActive(true); break;
            case 8:
                dialog[8].SetActive(false); MoveToMain();
                break;
        }

        storyID++;
    }

    public void MoveToMain()
    {
        SceneManager.LoadScene("00_Title");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StorySkip();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MoveToMain();
        }
    }
}
