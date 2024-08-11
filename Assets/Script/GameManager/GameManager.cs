using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
   public PoolManager pool;
   public LivingEntity player;
   
   private int score = 0;
   public bool isGameover { get; private set; }

   
   private void Start()
   {
      FindObjectOfType<PlayerHealth>().onDeath += EndGame;
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
}
