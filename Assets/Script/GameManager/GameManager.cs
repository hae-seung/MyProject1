using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
   private float min;
   private float sec;

   public bool isGameover { get; private set; }

   private void Start()
   {
      FindObjectOfType<PlayerHealth>().onDeath += EndGame;
   }

   private void Update()
   {
      sec += Time.deltaTime;
      if (sec >= 60f)
      {
         min += 1;
         sec = 0;
      }
      UIManager.Instance.UpdateTimeText(min, sec);
   }

   public void EndGame()
   {
      isGameover = true;
      UIManager.Instance.SetActiveGameoverUI(true);
   }
}
