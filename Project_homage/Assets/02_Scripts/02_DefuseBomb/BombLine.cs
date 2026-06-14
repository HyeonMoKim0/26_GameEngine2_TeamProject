using UnityEngine;
using UnityEngine.UI;

public class BombLine : MonoBehaviour
{
    public GameObject bomb;
    Bomb b;
    public Sprite cutted;

    // Start is called before the first frame update
    public enum wireType
    {
        Red,
        Yellow,
        Green,
        Blue
    }

    void Start()
    {
        b = bomb.GetComponent<Bomb>();
    }

    public void OnClickRedButton()
    {
        b.DefuseBomb(wireType.Red);
        BomBSound.instance.PlayCutSFX();
        CutLineImage();
    }

    public void OnClickYellowButton()
    {
        b.DefuseBomb(wireType.Yellow);
        BomBSound.instance.PlayCutSFX();
        CutLineImage();
    }

    public void OnClickGreenButton()
    {
        b.DefuseBomb(wireType.Green);
        BomBSound.instance.PlayCutSFX();
        CutLineImage();
    }

    public void OnClickBlueButton()
    {
        b.DefuseBomb(wireType.Blue);
        BomBSound.instance.PlayCutSFX();
        CutLineImage();
    }

    void CutLineImage()
    {
        GetComponent<Image>().sprite = cutted;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
