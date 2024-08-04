using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Mushroom : MonsterAI
{
   private float attackAnimationTime = 0.4f;
   public override void Setup(MonsterData monsterData)
   {
      base.Setup(monsterData);
      attackDelay += attackAnimationTime;
      criticalDamage = 2 * damage;
      bodyDamage = (float)(0.5 * damage);
   }

   protected override IEnumerator SetCriticalMotion()
   {
      Debug.Log("크리티컬!");
      yield return new WaitForSeconds(attackAnimationTime);
      monsterAnimator.SetBool("Critical", false);
      attackBox.AttackPlayer(criticalDamage);
      yield return new WaitForSeconds(1.0f);
      lastAttackTime = Time.time;
      isAttacking = false;
   }

   protected override IEnumerator SetAttackMotion()
   {
      yield return new WaitForSeconds(attackAnimationTime);
      monsterAnimator.SetBool("Attack", false);
      attackBox.AttackPlayer(damage);
      yield return new WaitForSeconds(1.0f);
      lastAttackTime = Time.time;
      isAttacking = false;
   }
   
   protected void BodyAttack()
   {
      if (Time.time >= lastAttackTime + attackDelay)
      {
         targetEntity.OnDamage(bodyDamage);
         lastAttackTime = Time.time;
      }
   }
   
   protected override void SetMonsterMove()
   {
      monsterAnimator.SetBool("Run", true);
   }

   protected override void SetMonsterIdle()
   {
      monsterAnimator.SetBool("Run", false);
   }
   

}
