using UnityEngine;

public class DisagreeButton : MonoBehaviour
{
    RectTransform buttonRect;

    public float buttonRange = 200;
    public float moveSpeed = 960f;

    // Start is called before the first frame update
    void Start()
    {
        buttonRect = GetComponent<RectTransform>();
        gameObject.SetActive(true);
    }

    public void Disagree()
    {
        if (AcceptManager.instance.isGame)
        {
            AcceptManager.instance.disagree = true;
            AcceptSound.instance.disagreeSFX.Play();
            AcceptManager.instance.disagreeText.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonPattern.instance.isButtonPattern2)
        {
            buttonRect.localScale += Vector3.one * Time.deltaTime * GameManager.instance.gameSpeed;
        }

        if (ButtonPattern.instance.isButtonPattern3)
        {
            buttonRect.transform.position -= Vector3.down * -720 * Time.deltaTime * GameManager.instance.gameSpeed;
        }
    }
}
