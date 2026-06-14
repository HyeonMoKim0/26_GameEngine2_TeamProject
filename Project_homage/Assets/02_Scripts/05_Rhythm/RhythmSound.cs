using UnityEngine;

public class RhythmSound : MonoBehaviour
{
    public static RhythmSound Instance;

    public AudioSource missSFX;
    public AudioSource clearSFX;


    void Awake()
    {
        Instance = this;
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
