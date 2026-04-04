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

        StartCoroutine(DisenableUI());
    }

    IEnumerator DisenableUI()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
