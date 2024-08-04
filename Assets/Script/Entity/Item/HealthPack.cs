using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HealthPack : MonoBehaviour, IItem
{

   private float heal = 50f;
   private LivingEntity livingEntity;
   
   public void Use(GameObject target)
   {
      livingEntity = target.GetComponent<LivingEntity>();
      if (livingEntity != null)
      {
         livingEntity.RestoreHealth(heal);
         AudioManager.Instance.playHeal();
      }
      Destroy(gameObject);
   }
}
