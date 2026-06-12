using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLine : MonoBehaviour
{
    public GameObject bomb;
    Bomb b;
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
        b = GetComponent<Bomb>();
    }

    public void OnClickRedButton()
    {
        b.DefuseBomb(wireType.Red);
        BomBSound.instance.PlayCutSFX();
    }

    public void OnClickYellowButton()
    {
        b.DefuseBomb(wireType.Yellow);
        BomBSound.instance.PlayCutSFX();
    }

    public void OnClickGreenButton()
    {
        b.DefuseBomb(wireType.Green);
        BomBSound.instance.PlayCutSFX();
    }

    public void OnClickBlueButton()
    {
        b.DefuseBomb(wireType.Blue);
        BomBSound.instance.PlayCutSFX();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
