using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BombCommandUI : MonoBehaviour
{
    public TextMeshProUGUI commandUI;

    // Start is called before the first frame update
    void Start()
    {
        commandUI = GetComponent<TextMeshProUGUI>();

        commandUI.text = SetCommand();
        StartCoroutine(DisenableTextUI());
    }

    IEnumerator DisenableTextUI()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }

    string SetCommand()
    {
        string[] commands = {
            "Cut the RED wires!",
            "Don't cut the RED wires!" };
        int index = Random.Range(0, commands.Length);
        return commands[index];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
