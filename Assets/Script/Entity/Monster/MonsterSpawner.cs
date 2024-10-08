using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterDataArray
{
    public MonsterData[] monsterData;
}

public class MonsterSpawner : MonoBehaviour
{
    public Transform bossSpawnPoint;
    public Transform[] spawnPoints;

    
    private int spawnCount = 0;
    private int currentMonsterCount = 0;

    private int spawnWave;
    private bool isBossAlive = false;
    private bool isWaveInProgress = false;
    
    [SerializeField] private List<MonsterDataArray> data;
    [SerializeField] private MonsterData[] bossData;
    [SerializeField] private GameObject[] bossPrefab;
    private List<MonsterAI> monsters = new();

    private void Start()
    {
        spawnWave = GameManager.Instance.Wave;
        GameManager.Instance.OnResume += StartWave;
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameover)
            return;

        UpdateUI();
    }

    public void StartWave()
    {
        spawnWave++;
        GameManager.Instance.Wave = spawnWave;
        isBossAlive = false; // 보스 상태 초기화

        int controlWave = 10;
        spawnCount = spawnWave < controlWave ? Mathf.RoundToInt(1.5f * spawnWave) : Mathf.RoundToInt(1.5f * controlWave);
        currentMonsterCount = spawnCount;

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnMonster();
        }
    }

    private void SpawnMonster()
    {
        int index = UnityEngine.Random.Range(0, data.Count);
        GameObject monsterObject = GameManager.Instance.pool.GetPrefab(index);

        monsterObject.transform.position = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].position;
        monsterObject.transform.rotation = Quaternion.identity;

        MonsterAI monster = monsterObject.GetComponent<MonsterAI>();
        if (monster != null)
        {
            int randStatus = UnityEngine.Random.Range(0, data[index].monsterData.Length); // 해당 인덱스 몬스터의 스크립터블 데이터 중 랜덤 선택
            monster.Setup(data[index].monsterData[randStatus]);
            monsters.Add(monster);

            monster.onDeath += () =>
            {
                if (monsters.Remove(monster))
                {
                    currentMonsterCount--; // 남아있는 몬스터 수 감소
                    GameManager.Instance.AddScore(spawnWave * 10);
                    // 모든 몬스터가 죽었을 때 보스 소환 준비
                    if (currentMonsterCount <= 0 && monsters.Count == 0 && !isBossAlive)
                    {
                        StartCoroutine(SpawnBossWithDelay());
                    }
                }
            };
        }
    }

    private IEnumerator SpawnBossWithDelay()
    {
        isBossAlive = true;
        
        yield return new WaitForSeconds(1.0f);
        UIManager.Instance.AnnounceBoss();

        int bossIndex = UnityEngine.Random.Range(0, bossData.Length);
        GameObject bossMonster = Instantiate(bossPrefab[bossIndex], bossSpawnPoint.position, Quaternion.identity);

        if (bossMonster != null)
            Debug.Log("보스가 소환되었습니다.");
        
        
        BossAI bossAI = bossMonster.GetComponent<BossAI>();
        bossAI.Setup(bossData[bossIndex]);
        monsters.Add(bossAI);

        bossAI.onDeath += () =>
        {
            monsters.Remove(bossAI);
            isBossAlive = false;
            GameManager.Instance.AddScore(spawnWave * 20);
            // 보스가 죽으면 다음 웨이브 시작
            UIManager.Instance.OpenShop();
        };
    }

    private void UpdateUI()
    {
        int remainingEnemies = isBossAlive ? 1 : Mathf.Max(0, currentMonsterCount); // 보스가 살아있으면 1, 아니면 남은 몬스터 수 (음수 방지)
        UIManager.Instance.UpdateWaveText(spawnWave, remainingEnemies);
    }
}
