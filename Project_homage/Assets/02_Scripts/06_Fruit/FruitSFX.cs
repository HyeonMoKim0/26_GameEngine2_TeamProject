using UnityEngine;

public class FruitSFX : MonoBehaviour
{
    public static FruitSFX Instance;

    public AudioSource dropSFX;
    public AudioSource evolutionSFX;

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
