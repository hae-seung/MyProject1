using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
   public float startingHealth = 100f;
   public float Health { get; protected set; }
   public bool Dead { get; protected set; }
   public event Action onDeath;
   
   protected virtual void OnEnable()
   {
      Dead = false;
      Health = startingHealth;
   }

   public virtual void RestoreHealth(float newHealth)
   {
      if (Dead)
      {
         return;
      }

      Health += newHealth;
   }
   
   public virtual void OnDamage(float damage)
   {
      Health -= damage;
      if (Health <= 0 && !Dead)
      {
         Die();
      }
   }

   protected virtual void Die()
   {
      onDeath?.Invoke();
      Dead = true;
   }
}
