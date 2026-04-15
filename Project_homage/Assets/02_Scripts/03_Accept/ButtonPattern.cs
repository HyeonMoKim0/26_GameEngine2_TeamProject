using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPattern : MonoBehaviour
{
    public GameObject acceptButton;
    public GameObject cancelButton;
    private RectTransform acceptRect;
    private RectTransform cancelRect;

    public float buttonRange = 200;
    public float moveSpeed = 960f;

    bool isButtonPattern1 = false;
    bool isButtonPattern2 = false;

    // Start is called before the first frame update
    void Start()
    {
        acceptRect = acceptButton.GetComponent<RectTransform>();
        cancelRect = cancelButton.GetComponent<RectTransform>();
    }

    void KeepButtonInScreen()
    {
        Vector3 pos = acceptRect.position;
        pos.x = Mathf.Clamp(pos.x, 0, Screen.width);
        pos.y = Mathf.Clamp(pos.y, 0, Screen.height);
        acceptRect.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (isButtonPattern1)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 buttonPos = acceptRect.position;

            float distance = Vector3.Distance(mousePos, buttonPos);

            if (distance < buttonRange)
            {
                Vector3 awayDirection = (buttonPos - mousePos).normalized;
                acceptRect.position += awayDirection * moveSpeed * Time.deltaTime;
                KeepButtonInScreen();
            }
        }

        if (isButtonPattern2)
        {
            cancelRect.localScale += Vector3.one * Time.deltaTime;
        }
    }
}
