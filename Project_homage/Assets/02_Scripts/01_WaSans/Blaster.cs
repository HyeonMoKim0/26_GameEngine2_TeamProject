using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float maxDistance;
    public float blastStartTime;
    public float removeTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Blast(blastStartTime));
        StartCoroutine(DestroyBlaster(removeTime));
    }

    // Blast 발사
    IEnumerator Blast(float blastTime)
    {
        yield return new WaitForSeconds(blastTime);
        lineRenderer.SetPosition(0, transform.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            lineRenderer.SetPosition(1, hit.point);

            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("적중!");
                Destroy(hit.collider.gameObject);
                GameManager.Instance.gameOver = true;
            }
        }
        else
        {
            lineRenderer.SetPosition(1, transform.position + (transform.forward * maxDistance));
        }
    }

    IEnumerator DestroyBlaster(float removeTime) // Blaster 자동 제거
    {
        yield return new WaitForSeconds(removeTime);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
