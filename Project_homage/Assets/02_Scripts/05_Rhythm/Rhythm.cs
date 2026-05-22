using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rhythm : MonoBehaviour
{
    public static Rhythm Instance;

    public Image redBall;
    public Image blueBall;

    [Header("Audio")]
    public AudioSource bgmSource;
    public float bpm = 128f;
    private double musicStartTimeDsp;
    private float musicStartTimeUnity;

    [Header("Map & Hierarchy")]
    public List<Tile> mapTiles = new List<Tile>();
    private int currentTileIndex = 1;

    [Header("MS Timing Judgment")]
    public float perfectThresholdMs = 50f;
    public float greatThresholdMs = 100f;

    private Transform currentPivot;
    private Transform currentOrbit;
    private float rotationSpeed;
    private bool isClockwise = true;

    // 배치 및 역산 핵심 변수
    private float startAngleOffset;
    private float orbitRadius;
    private float lastPivotAngle;
    private double lastPivotTargetTime; // ★ 유저 입력 시간이 아닌, 타일의 '절대적 정박 시간'을 저장합니다.

    void Start()
    {
        if (Instance == null) Instance = this;

        currentPivot = redBall.transform;
        currentOrbit = blueBall.transform;

        Vector3 initialOrbitLocalPos = currentOrbit.position - currentPivot.position;
        if (mapTiles.Count > 0)
            currentPivot.position = mapTiles[0].transform.position;
        currentOrbit.position = currentPivot.position + initialOrbitLocalPos;

        orbitRadius = Vector3.Distance(currentPivot.position, currentOrbit.position);
        Vector3 direction = currentOrbit.position - currentPivot.position;
        startAngleOffset = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (mapTiles.Count > 0)
        {
            mapTiles[0].targetAngle = startAngleOffset;
        }

        lastPivotAngle = startAngleOffset;

        float secondsPerBeat = 60f / bpm;
        rotationSpeed = 180f / secondsPerBeat;

        CalculateTileTargetTimes(secondsPerBeat);

        musicStartTimeDsp = AudioSettings.dspTime + 1.0f;
        musicStartTimeUnity = Time.time + 1.0f;

        // 0번 타일의 절대 목표 시간은 0입니다.
        lastPivotTargetTime = 0f;

        bgmSource.PlayScheduled(musicStartTimeDsp);
    }

    void CalculateTileTargetTimes(float secondsPerBeat)
    {
        double accumulatedTime = 0;
        if (mapTiles.Count > 0) mapTiles[0].targetTime = 0;

        for (int i = 1; i < mapTiles.Count; i++)
        {
            float angleInterval = Mathf.Abs(mapTiles[i].targetAngle - mapTiles[i - 1].targetAngle);
            if (angleInterval == 0) angleInterval = 180f;

            float beatRatio = angleInterval / 180f;
            accumulatedTime += (secondsPerBeat * beatRatio);

            mapTiles[i].targetTime = accumulatedTime;
        }
    }

    void Update()
    {
        if (!RhythmManager.Instance.isGame || Time.time < musicStartTimeUnity || currentTileIndex >= mapTiles.Count) return;

        // 음악이 재생된 지 몇 초 흘렀는지 계산 (절대 타임라인)
        float currentProgressTime = Time.time - musicStartTimeUnity;

        float directionMultiplier = isClockwise ? -1f : 1f;

        // ★ [핵심 변경] 유저가 언제 눌렀냐가 아니라, '이전 타일의 정박 시간'으로부터 음악이 얼마나 흘렀는지를 계산합니다.
        float timeSinceLastTileTarget = currentProgressTime - (float)lastPivotTargetTime;
        if (timeSinceLastTileTarget < 0f) timeSinceLastTileTarget = 0f;

        // 음악의 절대 시간에 완벽하게 맞물린 공의 각도 계산
        float currentAngle = lastPivotAngle + (timeSinceLastTileTarget * rotationSpeed * directionMultiplier);

        SetOrbitPositionByAngle(currentAngle);

        if (Input.anyKeyDown)
        {
            CheckMsTimingAndSwitch(currentProgressTime);
        }
    }

    void SetOrbitPositionByAngle(float angleDegrees)
    {
        float radians = angleDegrees * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f) * orbitRadius;
        currentOrbit.position = currentPivot.position + offset;
    }

    void CheckMsTimingAndSwitch(float currentProgressTime)
    {
        Tile targetTile = mapTiles[currentTileIndex];

        // 판정 역시 유저가 누른 시점(currentProgressTime)과 음악 정박(targetTile.targetTime)만 순수하게 비교
        float timeDiffInSeconds = currentProgressTime - (float)targetTile.targetTime;
        float timeDiffInMs = timeDiffInSeconds * 1000.0f;
        float absDiffMs = Mathf.Abs(timeDiffInMs);

        if (absDiffMs > 100f)
        {
            if (timeDiffInMs < 0)
                Debug.Log($"<color=red>[Game Over]</color> 너무 빠름! 오차: {timeDiffInMs:F1} ms");
            else
                Debug.Log($"<color=red>[Game Over]</color> 너무 느림! 오차: {timeDiffInMs:F1} ms");

            RhythmManager.Instance.gameOver = true;
            RhythmManager.Instance.isGame = false;
            bgmSource.Stop();
            return;
        }

        if (absDiffMs <= 50f)
            Debug.Log($"<color=cyan>정확 (Perfect!)</color> 오차: {timeDiffInMs:F1} ms");
        else if (timeDiffInMs < 0)
            Debug.Log($"<color=yellow>빠름 (Early)</color> 오차: {timeDiffInMs:F1} ms");
        else
            Debug.Log($"<color=orange>느림 (Late)</color> 오차: {timeDiffInMs:F1} ms");

        ProceedToNextTile();
    }

    void ProceedToNextTile()
    {
        currentTileIndex++;

        if (currentTileIndex >= mapTiles.Count)
        {
            Debug.Log("<color=yellow>모든 노트를 처리했습니다! 올 클리어!</color>");
            RhythmManager.Instance.gameClear = true;
            RhythmManager.Instance.isGame = false;
            return;
        }

        // 1. 축 교체 및 스위칭
        Transform previousPivot = currentPivot;
        currentPivot = currentOrbit;
        currentOrbit = previousPivot;

        // 시각적으로 튀는 현상을 방지하기 위해 피벗 위치를 이전 타일의 '정확한 중심점'으로 리셋
        currentPivot.position = mapTiles[currentTileIndex - 1].transform.position;

        // 2. 새로운 회전의 시작 각도를 정박 기준으로 세팅 (+180도 뒤에서 출발)
        lastPivotAngle = mapTiles[currentTileIndex - 1].targetAngle + 180f;

        // 3. 왼쪽에서 우측 전진이므로 시계 회전 강제 고정
        isClockwise = true;

        // ★ [가장 중요한 싱크 유지 코드] 
        // 다음 회전 연산의 기준 시간으로 유저가 누른 'Time.time'을 대입하지 않고,
        // 방금 통과한 타일의 '음악적 정박 타겟 시간'을 그대로 대입합니다.
        lastPivotTargetTime = mapTiles[currentTileIndex - 1].targetTime;
    }
}