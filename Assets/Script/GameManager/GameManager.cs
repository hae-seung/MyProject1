using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MySingleton<GameManager>
{
   public PoolManager pool;
   public LivingEntity player;
   public Transform spawnPoint;
   public MonsterSpawner monsterSpawner;
   
   public float money;
   
   public int score = 0;
   public bool isGameover { get; private set; }
   
   public int Wave { get; set; }
   
   public event Action OnResume;
   
   private void Start()
   {
      FindObjectOfType<PlayerHealth>().onDeath += EndGame;
      player.transform.position = spawnPoint.transform.position;
      money = 10000;//test
      UIManager.Instance.UpdateMoneyText(money);//test
      monsterSpawner.StartWave();
   }

   public void EndGame()
   {
      isGameover = true;
      UIManager.Instance.SetActiveGameoverUI(true);
   }

   public void AddScore(int newScore)
   {
      if (!isGameover)
      {
         score += newScore;
         UIManager.Instance.UpdateScoreText(score);
      }
   }

   public void PlayerSetSpawnPoint()
   {
      player.transform.position = spawnPoint.transform.position;
   }

   public void Resume()
   {
      OnResume?.Invoke();
      Time.timeScale = 1;
      
      Coin[] coins = FindObjectsOfType<Coin>();

      // 각각의 객체를 파괴합니다.
      foreach (Coin coin in coins)
      {
         Destroy(coin.gameObject);
      }
   }

   public bool Spend(float price)
   {
      if (money < price)
      {
         return false;
      }
      else
      {
         money -= price;
         UIManager.Instance.UpdateMoneyText(money);
         return true;
      }
   }

   public void AddMoney(float coin)
   {
      money += coin;
      UIManager.Instance.UpdateMoneyText(money);
   }

   public void SaveDiamond()
   {
      int diamondValue = Mathf.RoundToInt(score / 10);
      PlayerInfo.Instance.Diamond += diamondValue;
   }
}
