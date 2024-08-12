using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBoss : BossAI
{
    [SerializeField] private float attackAnimationTime = 0.4f;

    protected override void Awake()
    {
        base.Awake();
        deathAnimationDuration = 1.0f;
    }
   
    public override void Setup(MonsterData monsterData)
    {
        base.Setup(monsterData);
        attackDelay += attackAnimationTime;//기본 공격 딜레이를 1초로 주고 싶으면,애니메이션 끝나는 시간 + 1초를 포함해야함.
        criticalDamage = 2 * damage;
        bodyDamage = (float)(0.5 * damage);
    }

    protected override IEnumerator SetCriticalMotion()
    {
        Debug.Log("크리티컬!");
        yield return new WaitForSeconds(attackAnimationTime);
        monsterAnimator.SetBool("Critical", false);
        criticalBox.CriticalAttackPlayer(criticalDamage);
        yield return new WaitForSeconds(attackDelay);
        lastAttackTime = Time.time;
        isAttacking = false;
    }

    protected override IEnumerator SetAttackMotion()
    {
        yield return new WaitForSeconds(attackAnimationTime);
        monsterAnimator.SetBool("Attack", false);
        attackBox.AttackPlayer(damage);
        yield return new WaitForSeconds(attackDelay);
        lastAttackTime = Time.time;
        isAttacking = false;
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
