using System;
using System.Collections;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
   public float startingHealth = 100f;
   public float Health { get; protected set; }
   public bool Dead { get; protected set; }
   public event Action onDeath;

   //Hit Effect
   protected SpriteRenderer spriteRenderer;
   protected Color playerHalfA = new Color(1, 1, 1, 0.5f);
   protected Color monsterHalfA = new Color(1, 0, 0, 0.5f);
   protected Color FullA = new Color(1, 1, 1, 1);

   protected virtual void Awake()
   {
      spriteRenderer = GetComponent<SpriteRenderer>();
   }

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
      else
      {
         StartCoroutine(alphaBlink());
      }
   }

   protected virtual IEnumerator alphaBlink(){yield break;}

   protected virtual void Die()
   {
      onDeath?.Invoke();
      Dead = true;
   }
}
