using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI roundText;

    public void RestartGame()
    {
        SceneManager.LoadScene("00_Title");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        roundText.text = $"진행된 라운드 수 : {GameManager.instance.totalRound}";
    }
}
