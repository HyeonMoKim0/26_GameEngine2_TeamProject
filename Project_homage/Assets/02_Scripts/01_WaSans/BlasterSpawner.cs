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

    Vector3 spawnPos;
    Vector3 spawnDirection;
    Quaternion spawnRotation;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        InvokeRepeating("SpawnBlaster", spawnStart, spawnRate); 
        // Blast 생성 패턴들 랜덤으로 호출하도록 해야함

    }

    // Blast 생성 패턴화 해야함
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
        spawnPos = SpawnRandomPosition();
        spawnDirection = player.transform.position - spawnPos;
        spawnRotation = Quaternion.LookRotation(spawnDirection);
        blasterPrefab.GetComponent<Blaster>().blastStartTime = 0.5f;
        blasterPrefab.GetComponent<Blaster>().removeTime = 0.8f;
        Instantiate(blasterPrefab, spawnPos, spawnRotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (SansManager.Instance.gameOver || SansManager.Instance.gameClear)
        {
            if (IsInvoking("SpawnBlaster"))
            {
                CancelInvoke("SpawnBlaster");
            }
        }
    }
}
