using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
   public PoolManager pool;
   public LivingEntity player;
   public Transform spawnPoint;
   public float money;
   
   private int score = 0;
   public bool isGameover { get; private set; }
   public event Action OnResume;
   
   private void Start()
   {
      FindObjectOfType<PlayerHealth>().onDeath += EndGame;
      player.transform.position = spawnPoint.transform.position;
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
}
