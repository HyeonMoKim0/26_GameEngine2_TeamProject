using System;
using TMPro;
using UnityEngine;
using static BombLine;

public class Bomb : MonoBehaviour
{
    GameObject commandText;

    int countWire = 1;
    command trig;
    public enum command
    {
        CutRed,
        DontCutRed,
        CutBlue,
        DontCutBlue,
        CutGreen,
        DontCutGreen,
        CutYellow,
        DontCutYellow
    }

    // Start is called before the first frame update
    void Start()
    {
        commandText = GameObject.Find("Command Text");
        trig = SetDefuseTrigger();
    }

    command SetDefuseTrigger()
    {
        Array commands = Enum.GetValues(typeof(command));
        command randomCommand = (command)commands.GetValue(UnityEngine.Random.Range(0, commands.Length));

        switch (randomCommand)
        {
            case command.CutRed:
                commandText.GetComponent<TextMeshProUGUI>().text = "빨간 선을 잘라라!";
                break;
            case command.DontCutRed:
                commandText.GetComponent<TextMeshProUGUI>().text = "빨간 선을 자르지 마라!";
                break;
            case command.CutBlue:
                commandText.GetComponent<TextMeshProUGUI>().text = "파란 선을 잘라라!";
                break;
            case command.DontCutBlue:
                commandText.GetComponent<TextMeshProUGUI>().text = "파란 선을 자르지 마라!";
                break;
            case command.CutGreen:
                commandText.GetComponent<TextMeshProUGUI>().text = "초록 선을 잘라라!";
                break;
            case command.DontCutGreen:
                commandText.GetComponent<TextMeshProUGUI>().text = "초록 선을 자르지 마라!";
                break;
            case command.CutYellow:
                commandText.GetComponent<TextMeshProUGUI>().text = "노란 선을 잘라라!";
                break;
            case command.DontCutYellow:
                commandText.GetComponent<TextMeshProUGUI>().text = "노란 선을 자르지 마라!";
                break;
        }

        return randomCommand;
    }

    public void DefuseBomb(wireType clickedWire)
    {
        switch (trig)
        {
            case command.CutRed:
                if (clickedWire == wireType.Red)
                {
                    BombManager.instance.defused = true;
                }
                else
                    BombManager.instance.wrong = true;
                break;

            case command.DontCutRed:
                if (clickedWire != wireType.Red)
                {
                    BombManager.instance.defused = true;
                }
                else
                    BombManager.instance.wrong = true;
                break;
            case command.CutBlue:
                if (clickedWire == wireType.Blue)
                {
                    BombManager.instance.defused = true;
                }
                else
                    BombManager.instance.wrong = true;
                break;
            case command.DontCutBlue:
                if (clickedWire != wireType.Blue)
                {
                    BombManager.instance.defused = true;
                }
                else
                    BombManager.instance.wrong = true;
                break;
            case command.CutGreen:
                if (clickedWire == wireType.Green)
                {
                    BombManager.instance.defused = true;
                }
                else
                    BombManager.instance.wrong = true;
                break;
            case command.DontCutGreen:
                if (clickedWire != wireType.Green)
                {
                    BombManager.instance.defused = true;
                }
                else
                    BombManager.instance.wrong = true;
                break;
            case command.CutYellow:
                if (clickedWire == wireType.Yellow)
                {
                    BombManager.instance.defused = true;
                }
                else
                    BombManager.instance.wrong = true;
                break;
            case command.DontCutYellow:
                if (clickedWire != wireType.Yellow)
                {
                    BombManager.instance.defused = true;
                }
                else
                    BombManager.instance.wrong = true;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
