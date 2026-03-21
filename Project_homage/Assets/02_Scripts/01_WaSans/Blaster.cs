using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    public LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBlaster());
    }


    IEnumerator DestroyBlaster() // Blaster 자동 제거
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
