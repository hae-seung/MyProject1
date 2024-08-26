using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SniferQuest : MonoBehaviour
{
   public GameObject[] lockItem;
   public GameObject[] unlockItem;
   private Quest[] quests;
   public int price = 1;
   enum Quest
   {
      unlockDamage,
      unlockMaxAmmo
   };

   private void Awake()
   {
      quests = (Quest[])Enum.GetValues(typeof(Quest));
      if(!PlayerPrefs.HasKey("Data"))
         Init();
      if (PlayerPrefs.HasKey("UnlockQuest"))
      {
         UnlockItem();
         gameObject.SetActive(false);
      }
   }

   private void Init()
   {
      PlayerPrefs.SetInt("Data", 1);
      foreach (Quest quest in quests)
      {
         PlayerPrefs.SetInt(quest.ToString(), 0);
      }
   }

   private void UnlockItem()
   {
      for (int i = 0; i < lockItem.Length; i++)
      {
         string questName = quests[i].ToString();
         bool isUnlock = PlayerPrefs.GetInt(questName) == 1;
         lockItem[i].SetActive(!isUnlock);
         unlockItem[i].SetActive(isUnlock);
      }
   }

   public void ClickSniferQuest()
   {
      int curDiamond = PlayerPrefs.GetInt("Diamond");
      
      if(curDiamond >= price)
      {
         foreach (Quest quest in quests)
         {
            PlayerPrefs.SetInt(quest.ToString(), 1); //모두 해금 가능 상태로 표기
         }

         UnlockItem();
         PlayerPrefs.SetInt("Diamond", curDiamond - price);
         PlayerPrefs.SetInt("UnlockQuest", 1);
         gameObject.SetActive(false);
      }
   }
}
