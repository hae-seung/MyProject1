using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[System.Serializable]
public class MonsterDataArray
{
    public MonsterData[] monsterData;
}

public class MonsterSpawner : MonoBehaviour
{
    public Transform bossSpawnPoint;
    public Transform[] spawnPoints;

    private bool isBossAlive = false;
    private bool isWaveClear = false;
    private bool isSpawning = false; // 코루틴이 실행 중인지 확인하는 플래그
    private int wave = 0;
    private int spawnCount = 0;
    private int remainMonster = 0;

    [SerializeField] private List<MonsterDataArray> data;
    [SerializeField] private MonsterData[] bossData;
    private List<MonsterAI> monsters = new();


    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameover)
            return;
        
        if (isBossAlive)//보스 라운드에서는 Update 중지
            return;
        
        if (remainMonster == 0)//보스가 죽으면서 false로 전환할거임
            isWaveClear = true;
        
        if (monsters.Count <= 0 && !isSpawning) // 코루틴 실행 중에는 새로운 스폰을 막음
        {
            if (!isWaveClear)
                StartCoroutine(StartWave());
            else
                SpawnBoss(); //코루틴 걸면 늦게 스폰됨
        }
        UpdateUI();
    }

    private void SpawnBoss()
    {
        isBossAlive = true;
        Debug.Log("보스당");
        // 보스 스폰 로직 구현
    }
    
    private IEnumerator StartWave()
    {
        wave++;
        isSpawning = true; // 코루틴 시작 시 플래그 설정
        
        int controlWave = 10;
        if (wave < 10)
            spawnCount = Mathf.RoundToInt(1.5f * wave);
        else
            spawnCount = Mathf.RoundToInt(1.5f * controlWave);
        
        remainMonster = spawnCount;

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnMonster();
            yield return new WaitForSeconds(0.2f); // 코루틴을 통해 일정 시간 간격으로 몬스터 스폰
        }

        isSpawning = false; // 코루틴 종료 시 플래그 해제
    }

    private void SpawnMonster()
    {
        int index = Random.Range(0, 2); //2 = > temp
        GameObject monsterObject = GameManager.Instance.pool.GetPrefab(index); // 몬스터 종류는 현재 2개
        monsterObject.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

        MonsterAI monster = monsterObject.GetComponent<MonsterAI>();
        if (monster != null)
        {
            int randStatus = Random.Range(0, data[index].monsterData.Length); // 해당 인덱스 몬스터의 스크립터블 데이터 중 랜덤 선택
            monster.Setup(data[index].monsterData[randStatus]);
            monsters.Add(monster);
            
            monster.onDeath += () => 
            {
                monsters.Remove(monster);
                GameManager.Instance.AddScore(20);
                remainMonster--;
            };
        }
    }

    private void UpdateUI()
    {
        UIManager.Instance.UpdateWaveText(wave, monsters.Count);
    }
}
