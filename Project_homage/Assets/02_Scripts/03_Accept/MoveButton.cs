using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButton : MonoBehaviour
{
    public GameObject acceptButton;
    private RectTransform buttonRect;

    public float buttonRange = 200;
    public float moveSpeed = 960f;

    // Start is called before the first frame update
    void Start()
    {
        buttonRect = acceptButton.GetComponent<RectTransform>();
    }

    void KeepButtonInScreen()
    {
        Vector3 pos = buttonRect.position;
        pos.x = Mathf.Clamp(pos.x, 0, Screen.width);
        pos.y = Mathf.Clamp(pos.y, 0, Screen.height);
        buttonRect.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 buttonPos = buttonRect.position;

        float distance = Vector3.Distance(mousePos, buttonPos);

        if (distance < buttonRange)
        {
            Vector3 awayDirection = (buttonPos - mousePos).normalized;
            buttonRect.position += awayDirection * moveSpeed * Time.deltaTime;
            KeepButtonInScreen();
        }
    }
}
