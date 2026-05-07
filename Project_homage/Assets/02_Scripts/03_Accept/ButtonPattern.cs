using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPattern : MonoBehaviour
{
    public GameObject agreeButton;
    public GameObject disagreeButton;
    private RectTransform agreeRect;
    private RectTransform disagreeRect;

    public float buttonRange = 200;
    public float moveSpeed = 960f;

    bool isButtonPattern1 = false;
    bool isButtonPattern2 = false;

    // Start is called before the first frame update
    void Start()
    {
        agreeRect = agreeButton.GetComponent<RectTransform>();
        disagreeRect = disagreeButton.GetComponent<RectTransform>();

        RandomAgree();
    }

    void RandomAgree()
    {
        int randomPattern = Random.Range(1, 3);
        switch (randomPattern)
        {
            case 1:
                ButtonPattern1();
                break;
            case 2:
                ButtonPattern2();
                break;
        }
    }

    Vector3 RandomPos()
    {
        float randomX = Random.Range(-Screen.width / 2, Screen.width / 2);
        float randomY = Random.Range(-Screen.height / 2, Screen.height / 2);

        return new Vector3(randomX, randomY, 0);
    }

    void ButtonPattern1()
    {
        agreeButton.SetActive(true);
        agreeRect.position = RandomPos();

        isButtonPattern1 = true;
    }

    void ButtonPattern2()
    {
        agreeButton.SetActive(true);
        disagreeButton.SetActive(false);
        agreeRect.position = new Vector3(-200, 200, 0);
        disagreeRect.position = new Vector3(200, 200, 0);

        isButtonPattern2 = true;
    }

    void KeepButtonInScreen()
    {
        Vector3 pos = agreeRect.position;
        pos.x = Mathf.Clamp(pos.x, 0, Screen.width);
        pos.y = Mathf.Clamp(pos.y, 0, Screen.height);
        agreeRect.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (isButtonPattern1)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 buttonPos = agreeRect.position;

            float distance = Vector3.Distance(mousePos, buttonPos);

            if (distance < buttonRange)
            {
                Vector3 awayDirection = (buttonPos - mousePos).normalized;
                agreeRect.position += awayDirection * moveSpeed * Time.deltaTime;
                KeepButtonInScreen();
            }
        }

        if (isButtonPattern2)
        {
            disagreeRect.localScale += Vector3.one * Time.deltaTime;
        }
    }
}
