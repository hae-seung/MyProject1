using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunUpgrade : MonoBehaviour
{
   private int level = 0;
   public Text levelText;
   public Image icon;
   public Gun[] guns;
   public Text AmmoText;
   private void Awake()
   {
      
   }
   
   public void RnageUpgrade(int index)
   {
      level++;
      switch (index)
      {
         case 0 :
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
      
      if (level == 5)
      {
         GetComponent<Button>().interactable = false;
      }
   }

   public void DamageUpgrade(int index)
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
      
      if (level == 5)
      {
         GetComponent<Button>().interactable = false;
      }
   }

   public void AddAmmoCapacity(int index)
   {
      switch (index)
      {
         case 0:
            guns[index].AmmoCapacity += 30;
            break;
         case 1:
            guns[index].AmmoCapacity += 15;
            break;
         case 2:
            guns[index].AmmoCapacity += 5;
            break;
      }
      
      AmmoText.text = guns[index].AmmoCapacity.ToString();
      
      UIManager.Instance.UpdateGunModeText(guns[index].ToString());
      UIManager.Instance.UpdateAmmoText(guns[index].MagAmmo, guns[index].AmmoCapacity);
   }
}
