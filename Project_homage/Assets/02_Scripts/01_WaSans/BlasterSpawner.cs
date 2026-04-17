using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BlasterSpawner : MonoBehaviour
{
    public GameObject blasterPrefab;
    private GameObject player;
    private Coroutine patternCoroutine;

    private float xRange = 25;
    private float zRange = 10;

    Vector3 spawnPos;
    Vector3 spawnDir;
    Quaternion spawnRot;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        // 생성 패턴을 랜덤으로 호출하도록
        // 패턴이 종료되었을 때 ?초의 대기시간 후 다시 랜덤 패턴 실행
        RandomBlastPattern();
    }

    void RandomBlastPattern()
    {
        int randomBlast = Random.Range(1, 3);
        switch (randomBlast)
        {
            case 1:
                patternCoroutine =
                    StartCoroutine(SpawnBlasterRoutine1());
                Invoke(nameof(StopPattern), 7f);
                break;
            case 2:
                patternCoroutine =
                    StartCoroutine(SpawnBlasterRoutine2());
                Invoke(nameof(StopPattern), 9f);
                break;
        }
    }

    IEnumerator SpawnBlasterRoutine1()
    {
        float spawnRate = 0.4f;
        while (true)
        {
            SpawnBlaster1();

            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void SpawnBlaster1() // 무작위 Blaster 생성 패턴
    {
        float spawnPosX = Random.Range(-xRange, xRange);
        float spawnPosZ = Random.Range(-zRange, zRange);

        spawnPos = new Vector3(spawnPosX, 2, spawnPosZ);
        spawnDir = player.transform.position - spawnPos;
        spawnRot = Quaternion.LookRotation(spawnDir);

        blasterPrefab.GetComponent<Blaster>().blastStartTime = 0.5f;
        blasterPrefab.GetComponent<Blaster>().removeTime
            = blasterPrefab.GetComponent<Blaster>().blastStartTime + 0.3f;
        
        Instantiate(blasterPrefab, spawnPos, spawnRot);
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
        spawnDir = new Vector3(0, 2, 0) - spawnPos;
        spawnRot = Quaternion.LookRotation(spawnDir);

        blasterPrefab.GetComponent<Blaster>().blastStartTime = 1f;
        blasterPrefab.GetComponent<Blaster>().removeTime =
            blasterPrefab.GetComponent<Blaster>().blastStartTime + 0.2f;

        Instantiate(blasterPrefab, spawnPos, spawnRot);
    }

    void StopPattern()
    {
        if (patternCoroutine != null)
        {
            StopCoroutine(patternCoroutine);
            patternCoroutine = null;
        }

        // 패턴 종료 1초 후 랜덤 패턴 실행
        Invoke(nameof(RandomBlastPattern), 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!SansManager.Instance.isGame)
        {
            StopAllCoroutines();
        }
    }
}
