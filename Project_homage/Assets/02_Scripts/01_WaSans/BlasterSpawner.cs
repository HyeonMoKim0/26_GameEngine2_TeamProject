using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterSpawner : MonoBehaviour
{
    public GameObject blasterPrefab;
    private GameObject player;

    public float spawnStart;
    public float spawnRate;
    private float xRange = 25;
    private float zRange = 10;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        InvokeRepeating("SpawnBlaster", spawnStart, spawnRate);
    }

    private void BlastPattern1()
    {

    }

    private Vector3 SpawnRandomPosition() // Blaster 스폰 위치 랜덤 지정
    {
        float spawnPosX = Random.Range(-xRange, xRange);
        float spawnPosZ = Random.Range(-zRange, zRange);
        Vector3 randomPos = new Vector3(spawnPosX, 2, spawnPosZ);

        return randomPos;
    }

    void SpawnBlaster() // Blaster 스폰
    {
        Vector3 spawnPos = SpawnRandomPosition();
        Vector3 direction = player.transform.position - spawnPos;
        Quaternion rotation = Quaternion.LookRotation(direction);
        Instantiate(blasterPrefab, spawnPos, rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameOver || GameManager.Instance.gameClear)
        {
            if (IsInvoking("SpawnBlaster"))
            {
                CancelInvoke("SpawnBlaster");
            }
        }
    }
}
