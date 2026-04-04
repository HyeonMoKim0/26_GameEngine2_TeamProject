using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterSpawner : MonoBehaviour
{
    public GameObject blasterPrefab;
    private GameObject player;
    private Coroutine patternCoroutine;

    private float xRange = 25;
    private float zRange = 10;

    Vector3 spawnPos;
    Vector3 spawnDirection;
    Quaternion spawnRotation;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        // Blast 생성 패턴들 랜덤으로 호출하도록 해야함
        StartBlastPattern2();
    }

    public void StartBlastPattern1()
    {
        patternCoroutine = StartCoroutine(SpawnBlasterRoutine1());
        Invoke("StopPattern", 7f);
    }

    IEnumerator SpawnBlasterRoutine1()
    {
        float spawnRate = 0.4f;
        while (true)
        {
            float spawnPosX = Random.Range(-xRange, xRange);
            float spawnPosZ = Random.Range(-zRange, zRange);
            spawnPos = new Vector3(spawnPosX, 2, spawnPosZ);
            BlastPattern1(spawnPos);

            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void BlastPattern1(Vector3 spawnPos) // 무작위 Blaster 생성 패턴
    {
        spawnDirection = player.transform.position - spawnPos;
        spawnRotation = Quaternion.LookRotation(spawnDirection);
        blasterPrefab.GetComponent<Blaster>().blastStartTime = 0.5f;
        blasterPrefab.GetComponent<Blaster>().removeTime = blasterPrefab.GetComponent<Blaster>().blastStartTime + 0.3f;
        Instantiate(blasterPrefab, spawnPos, spawnRotation);
    }

    public void StartBlastPattern2()
    {
        StartCoroutine(SpawnBlasterRoutine2());
        Invoke("StopPattern", 9f);
    }

    private float r = 10f;
    private float currentAngle = 0f; // 현재 각도
    private float angleStep = 18f;   // 한 번에 회전할 각도 (조정 가능)

    IEnumerator SpawnBlasterRoutine2()
    {
        while (true)
        {
            SpawnBlaster2();
            // 각도를 증가시켜 반시계 방향으로 회전 (각도가 커질수록 Sin은 양수가 되어 +X로 이동)
            currentAngle += angleStep;

            yield return new WaitForSeconds(0.07f);
        }
    }

    private void SpawnBlaster2()
    {
        float radian = currentAngle * Mathf.Deg2Rad;
        spawnPos = new Vector3(Mathf.Sin(radian) * r, 2f, Mathf.Cos(radian) * r);
        spawnDirection = new Vector3(0, 2, 0) - spawnPos;
        spawnRotation = Quaternion.LookRotation(spawnDirection);
        blasterPrefab.GetComponent<Blaster>().blastStartTime = 1f;
        blasterPrefab.GetComponent<Blaster>().removeTime = blasterPrefab.GetComponent<Blaster>().blastStartTime + 0.2f;
        Instantiate(blasterPrefab, spawnPos, spawnRotation);
    }

    void StopPattern()
    {
        if (patternCoroutine != null)
        {
            StopCoroutine(patternCoroutine);
            patternCoroutine = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SansManager.Instance.gameOver || SansManager.Instance.gameClear)
        {
            StopAllCoroutines();
        }
    }
}
