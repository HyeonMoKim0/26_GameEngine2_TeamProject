using UnityEngine;

public class CloseGame : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("게임 종료 요청됨");

        // 1. 유니티 에디터 환경에서 플레이 중일 때
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        // 2. PC(Windows/Mac) 프로그램이나 모바일 앱으로 빌드된 상태일 때
#else
        Application.Quit();
#endif
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
