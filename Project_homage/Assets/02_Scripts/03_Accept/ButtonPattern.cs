using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPattern : MonoBehaviour
{
    static public ButtonPattern instance;
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

    public GameObject agreeButton;
    public GameObject disagreeButton;
    public GameObject agreeCanvas;

    public GameObject agree5;
    public GameObject disagree5;

    [Header("PatternBool")]
    public bool isButtonPattern1 = false;
    public bool isButtonPattern2 = false;
    public bool isButtonPattern3 = false;
    public bool isButtonPattern4 = false;
    public bool isButtonPattern5 = false;

    public GameObject pattern4Canvas;
    public GameObject[] pattern4Path = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {
        RandomAgree();
    }

    void RandomAgree()
    {
        int randomPattern = Random.Range(1, 6);
        switch (randomPattern)
        {
            case 1:
                ButtonPattern1();
                Debug.Log("도망치는 동의 버튼");
                break;
            case 2:
                ButtonPattern2();
                Debug.Log("점점 커지는 비동의 버튼");
                break;
            case 3:
                StartCoroutine(ButtonPattern3());
                Debug.Log("쏟아지는 동의 버튼");
                break;
            case 4:
                ButtonPattern4();
                Debug.Log("동의 면접");
                break;
            case 5:
                ButtonPattern5();
                Debug.Log("연타 동의");
                break;
        }
    }

    Vector3 RandomPos()
    {
        float randomX = Random.Range(100, Screen.width-100);
        float randomY = Random.Range(50, Screen.height-50);

        return new Vector3(randomX, randomY, 0);
    }

    void ButtonPattern1()
    {
        AcceptManager.instance.currentTime = 5f;

        isButtonPattern1 = true;

        GameObject agree = Instantiate(agreeButton, RandomPos(), Quaternion.identity, agreeCanvas.transform);
    }

    void ButtonPattern2()
    {
        AcceptManager.instance.currentTime = 5f;

        isButtonPattern2 = true;

        Vector3 agreePos = new Vector3(Screen.width / 32 * 13, Screen.height / 5, 0);
        Vector3 disagreePos = new Vector3(Screen.width / 32 * 19, Screen.height / 5, 0);

        GameObject agree = Instantiate(agreeButton, agreePos, Quaternion.identity, agreeCanvas.transform);
        GameObject disagree = Instantiate(disagreeButton, disagreePos, Quaternion.identity, agreeCanvas.transform);
    }

    IEnumerator ButtonPattern3()
    {
        AcceptManager.instance.currentTime = 10f;

        isButtonPattern3 = true;

        int agreeIdx = 0;
        while (AcceptManager.instance.isGame)
        {
            yield return new WaitForSeconds(0.2f);
            Vector3 randomPos =
                new Vector3(Random.Range(100, Screen.width - 100), Screen.height + 100, 0);
            if (agreeIdx == 5)
            {
                agreeIdx = 0;
                GameObject agree = Instantiate(agreeButton, randomPos, Quaternion.identity, agreeCanvas.transform);
            }
            else
            {
                agreeIdx++;
                GameObject disagree = Instantiate(disagreeButton, randomPos, Quaternion.identity, agreeCanvas.transform);
            }
        }
    }

    void ButtonPattern4()
    {
        AcceptManager.instance.currentTime = 10f;

        Vector3 agreePos = new Vector3(Screen.width / 32 * 13, Screen.height / 5, 0);
        Vector3 disagreePos = new Vector3(Screen.width / 32 * 19, Screen.height / 5, 0);

        GameObject agree = Instantiate(agreeButton, agreePos, Quaternion.identity, agreeCanvas.transform);
        GameObject disagree = Instantiate(disagreeButton, disagreePos, Quaternion.identity, agreeCanvas.transform);

        pattern4Canvas.SetActive(true);
        pattern4Path[0].SetActive(true);
        pattern4Path[1].SetActive(true);
        pattern4Path[2].SetActive(true);
        pattern4Path[3].SetActive(true);

        isButtonPattern4 = true;
    }

    void ButtonPattern5()
    {
        AcceptManager.instance.currentTime = 5f;

        Vector3 agreePos = new Vector3(Screen.width / 32 * 13, Screen.height / 5, 0);
        Vector3 disagreePos = new Vector3(Screen.width / 32 * 19, Screen.height / 5, 0);

        agree5 = Instantiate(agreeButton, agreePos, Quaternion.identity, agreeCanvas.transform);
        disagree5 = Instantiate(disagreeButton, disagreePos, Quaternion.identity, agreeCanvas.transform);

        isButtonPattern5 = true;
    }

    public void ChangeButton(GameObject agree, GameObject disagree)
    {
        Vector3 temp = agree.GetComponent<RectTransform>().anchoredPosition;
        agree.GetComponent<RectTransform>().anchoredPosition
            = disagree.GetComponent<RectTransform>().anchoredPosition;
        disagree.GetComponent<RectTransform>().anchoredPosition
            = temp;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
