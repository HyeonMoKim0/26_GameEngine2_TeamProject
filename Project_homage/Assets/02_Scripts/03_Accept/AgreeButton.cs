using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgreeButton : MonoBehaviour
{
    RectTransform buttonRect;

    public float buttonRange = 200;
    public float moveSpeed = 960f;

    float buttonGage = 0;

    // Start is called before the first frame update
    void Start()
    {
        buttonRect = GetComponent<RectTransform>();
        gameObject.SetActive(true);
    }

    public void Agree()
    {
        if (AcceptManager.instance.isGame)
        {
            if (ButtonPattern.instance.isButtonPattern5)
            {
                buttonGage += 10;
                if (Random.Range(0, 10) < 3)
                {
                    ButtonPattern.instance.ChangeButton(
                        ButtonPattern.instance.agree5,
                        ButtonPattern.instance.disagree5);
                }
                if (buttonGage < 100) return;
            }

            AcceptManager.instance.agree = true;
            AcceptSound.instance.agreeSFX.Play();
            AcceptManager.instance.agreeText.SetActive(true);
        }
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
        if (AcceptManager.instance.isGame)
        {
            if (ButtonPattern.instance.isButtonPattern1)
            {
                Vector3 mousePos = Input.mousePosition;
                Vector3 buttonPos = buttonRect.position;

                float distance = Vector3.Distance(mousePos, buttonPos);

                if (distance < buttonRange)
                {
                    Vector3 awayDirection = (buttonPos - mousePos).normalized;
                    buttonRect.position += awayDirection * moveSpeed * Time.deltaTime * GameManager.instance.gameSpeed;
                    KeepButtonInScreen();
                }
            }

            if (ButtonPattern.instance.isButtonPattern3)
            {
                buttonRect.transform.position -= Vector3.down * -270 * Time.deltaTime * GameManager.instance.gameSpeed;
            }

            if (ButtonPattern.instance.isButtonPattern5)
            {
                buttonGage -= 2 * Time.deltaTime * GameManager.instance.gameSpeed;
            }
        }
    }
}
