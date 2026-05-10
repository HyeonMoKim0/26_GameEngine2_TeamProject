using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BlasterSpawner : MonoBehaviour
{
    public GameObject blasterPrefab;
    private GameObject player;
    private Coroutine patternCoroutine;

    private float xRange = 15;
    private float zRange = 15;

    Vector3 spawnPos;
    Vector3 spawnDir;
    Quaternion spawnRot;

    public float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        RandomBlastPattern();
    }

    void RandomBlastPattern()
    {
        int randomBlast = Random.Range(3, 4);
        switch (randomBlast)
        {
            case 1:
                patternCoroutine =
                    StartCoroutine(SpawnBlasterRoutine1());
                SansManager.Instance.currentTime = 8f;
                break;
            case 2:
                patternCoroutine =
                    StartCoroutine(SpawnBlasterRoutine2());
                SansManager.Instance.currentTime = 10f;
                break;
            case 3:
                patternCoroutine =
                    StartCoroutine(SpawnBlasterRoutine3());
                SansManager.Instance.currentTime = 4f;
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

        // Spawn Position, Direction, Rotation
        spawnPos = new Vector3(spawnPosX, 2, spawnPosZ);
        spawnDir = player.transform.position - spawnPos;
        spawnRot = Quaternion.LookRotation(spawnDir);

        // Blast Start Time, Remove Time
        blasterPrefab.GetComponent<Blaster>().blastStartTime = 0.5f;
        blasterPrefab.GetComponent<Blaster>().removeTime
            = blasterPrefab.GetComponent<Blaster>().blastStartTime + 0.3f;
        
        Instantiate(blasterPrefab, spawnPos, spawnRot);
    }

    private float r = 9f;
    private float currentAngle = 0f; // 현재 각도
    private float angleStep = 18f;   // 한 번에 회전할 각도 (조정 가능)

    IEnumerator SpawnBlasterRoutine2()
    {
        float spawnRate = 0.07f;
        while (true)
        {
            SpawnBlaster2();
            // 각도를 증가시켜 반시계 방향으로 회전 (각도가 커질수록 Sin은 양수가 되어 +X로 이동)
            currentAngle += angleStep;

            yield return new WaitForSeconds(spawnRate);
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

    int spawnCount = 0;

    IEnumerator SpawnBlasterRoutine3()
    {
        float spawnRate = 0.65f;
        while (spawnCount < 4)
        {
            spawnCount++;
            SpawnBlaster3(spawnCount);

            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void SpawnBlaster3(int count)
    {
        // Blast Start Time, Remove Time
        blasterPrefab.GetComponent<Blaster>().blastStartTime = 0.4f;
        blasterPrefab.GetComponent<Blaster>().removeTime =
            blasterPrefab.GetComponent<Blaster>().blastStartTime + 0.25f;

        switch (count)
        {
            case 1:
            case 3:
                // Spawn Position, Direction, Rotation
                Vector3[] spawnPos_1 =
                {
                    new Vector3(-12, 2, 14),
                    new Vector3(-14, 2, 12),
                    new Vector3(14, 2, -12),
                    new Vector3(12, 2, -14),
                };

                Vector3[] spawnRot_1 =
                {
                    Vector3.back,
                    Vector3.right,
                    Vector3.left,
                    Vector3.forward
                };

                for (int i = 0; i < 4; i++)
                {
                    spawnPos = spawnPos_1[i];
                    spawnRot = Quaternion.LookRotation(spawnRot_1[i]);
                    Instantiate(blasterPrefab, spawnPos, spawnRot);
                }
                break;
            case 2:
                // Spawn Position, Direction, Rotation
                Vector3[] spawnPos_2 =
                {
                    new Vector3 (-14, 2, 14),
                    new Vector3 (14, 2, 14)
                };

                Vector3[] spawnRot_2 =
                {
                    new Vector3(1, 0, -1),
                    new Vector3(-1, 0, -1)
                };

                for (int i = 0; i < 2; i++)
                {
                    spawnPos = spawnPos_2[i];
                    spawnRot = Quaternion.LookRotation(spawnRot_2[i]);
                    Instantiate(blasterPrefab, spawnPos, spawnRot);
                }
                break;
            case 4:
                // Spawn Position, Direction, Rotation
                Vector3[] spawnPos_4 =
                {
                    new Vector3(-14, 2, 0),
                    new Vector3(14, 2, 0)
                };
                Vector3[] spawnRot_4 =
                {
                    Vector3.right,
                    Vector3.left
                };
                for (int i = 0; i < 2; i++)
                {
                    spawnPos = spawnPos_4[i];
                    spawnRot = Quaternion.LookRotation(spawnRot_4[i]);
                    Instantiate(blasterPrefab, spawnPos, spawnRot);
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!SansManager.Instance.isGame)
        {
            StopCoroutine(patternCoroutine);
        }
    }
}
