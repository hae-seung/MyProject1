using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunUpgrade : MonoBehaviour
{
   private int level = 0;
   public Text levelText;
   public Text goldText;
   public Gun[] guns;
   public Text AmmoText;

   private float price = 50f;
   
   private void Awake()
   {
      
   }
   
   public void RnageUpgrade(int index)
   {
      if(GameManager.Instance.Spend(price))
      {
         level++;
         switch (index)
         {
            case 0:
               guns[index].BulletMaxDistance += 5f;
               break;
            case 1:
               guns[index].BulletMaxDistance += 5f;
               break;
            case 2:
               guns[index].BulletMaxDistance += 10f;
               break;
         }

         levelText.text = "Lv." + level;
         goldText.text = guns[index].ToString() + ":" + price;
         price *= 1.5f;
      }
      
      if (level == 5)
      {
         GetComponent<Button>().interactable = false;
      }
   }

   public void DamageUpgrade(int index)
   {
      if(GameManager.Instance.Spend(price))
      {
         level++;
         switch (index)
         {
            case 0:
               guns[index].BulletDamage += 20;
               break;
            case 1:
               guns[index].BulletDamage += 10;
               break;
            case 2:
               guns[index].BulletDamage += 50;
               break;
         }

         levelText.text = "Lv." + level;
         goldText.text = guns[index].ToString() + ":" + price;
         price *= 1.5f;
      }
      
      if (level == 5)
      {
         GetComponent<Button>().interactable = false;
      }
   }

   public void AddAmmoCapacity(int index)
   {
      if(GameManager.Instance.Spend(price))
      {
         switch (index)
         {
            case 0:
               guns[index].AmmoCapacity += 30;
               UIManager.Instance.UpdateGunModeText("Rifle");
               break;
            case 1:
               guns[index].AmmoCapacity += 15;
               UIManager.Instance.UpdateGunModeText("Shotgun");
               break;
            case 2:
               guns[index].AmmoCapacity += 5;
               UIManager.Instance.UpdateGunModeText("Snifer");
               break;
         }

         UIManager.Instance.UpdateAmmoText(guns[index].MagAmmo, guns[index].AmmoCapacity);
         
      }
   }
}
