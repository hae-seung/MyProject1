using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
   [SerializeField]
   private int critical;
   [SerializeField]
   private int money;
   
   private void Awake()
   {
      critical = 0;
      money = 0;
   }
   
   public int Critical
   {
      get
      {
         return critical;
      }
      set
      {
         critical = value;
      }
   }

   public int Money
   {
      get
      {
         return money;
      }
      set
      {
         money = value;
      }
   }
}
