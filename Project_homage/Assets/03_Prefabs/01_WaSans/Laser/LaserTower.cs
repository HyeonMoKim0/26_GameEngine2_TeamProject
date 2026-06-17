using UnityEngine;
using System.Collections;

public class LaserTower : MonoBehaviour
{
    public LineRenderer laser;

    void Start()
    {
        StartCoroutine(FireLaser());
    }

    IEnumerator FireLaser()
    {
        while (true)
        {
            laser.enabled = true; // 레이저 켜기

            yield return new WaitForSeconds(0.1f); // 0.1초 유지

            laser.enabled = false; // 레이저 끄기

            yield return new WaitForSeconds(1f); // 1초 대기
        }
    }
}