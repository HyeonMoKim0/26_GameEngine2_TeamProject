using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer; // 인스펙터에서 'Player'만 체크하세요.
    public LineRenderer lineRenderer;
    public float maxDistance;
    public float blastStartTime;
    public float removeTime;
    public float laserWidth;
    bool isBlast = false;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(Blast(blastStartTime));
        StartCoroutine(DestroyBlaster(removeTime));
    }

    // Blast 발사
    IEnumerator Blast(float blastTime)
    {
        yield return new WaitForSeconds(blastTime);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.enabled = true;

        isBlast = true;
    }

    IEnumerator DestroyBlaster(float removeTime) // Blaster 자동 제거
    {
        yield return new WaitForSeconds(removeTime);
        isBlast = false;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (isBlast)
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, laserWidth / 2, transform.forward, out hit, maxDistance, targetLayer))
            {
                lineRenderer.SetPosition(1, hit.point);

                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("적중!");
                    Destroy(hit.collider.gameObject);
                    SansManager.Instance.gameOver = true;
                }
            }
            else
            {
                lineRenderer.SetPosition(1, transform.position + (transform.forward * maxDistance));
            }
        }
    }
}
