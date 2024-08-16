using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Timeline;

public class MonsterAI : LivingEntity
{
    protected Transform targetTransform;
    protected LivingEntity targetEntity;
    
    //scriptable data => All temp
    protected float targetLockOnDistance;
    protected float speed;
    protected float damage;
    protected int criticalProbability;
    protected float attackDelay; //공격이 끝나고 그 다음 공격까지 시간
    
    //Inheritance
    protected float criticalDamage;
    protected float bodyDamage;
    protected float deathAnimationDuration;
    public float lastAttackTime = 0.0f;//fixed
    
    //state
    protected float randomMoveInterval = 3.0f; // Random move time interval, temp, fixed
    protected float nextMoveTime = 0.0f; // Frame time, fixed
    protected int currentDirection;
    public bool isAttacking = false;
    protected bool facingRight = true;
    
    //component
    protected Animator monsterAnimator;
    protected AudioSource monsterAudioSource;
    protected Rigidbody2D monsterRigidbody;
    protected ProbabilityCalculator probabilityCalculator = new();


    [SerializeField]protected GameObject coinPrefab;
    [SerializeField]protected AudioClip hitSound;
    [SerializeField]protected AudioClip deathSound;
    [SerializeField]protected AttackBox attackBox;
    [SerializeField]protected CriticalBox criticalBox;
    
    protected bool aliveTarget
    {
        get
        {
            if (targetEntity != null && !targetEntity.Dead)
            {
                return true;
            }
            return false;
        }
    }

    protected override void OnEnable()
    {
        Init();
    }

    protected void Init()
    {
        // Target 초기화
        targetEntity = GameManager.Instance.player;
        targetTransform = targetEntity.transform;
    
        // 기본 상태 초기화
        Dead = false;
        isAttacking = false;
        lastAttackTime = 0;

        // Collider 초기화
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = true;
        }

        // 애니메이터 초기화
        monsterAnimator.ResetTrigger("Die");
        monsterAnimator.SetBool("Attack", false);
        monsterAnimator.SetBool("Critical", false);
        monsterAnimator.Play("Idle", -1, 0f); // 애니메이터를 초기화

        // 물리적 상태 초기화
        monsterRigidbody.velocity = Vector2.zero;
    
        // 기타 초기화
        nextMoveTime = 0;
        currentDirection = 0;
    }
    
    protected override void Awake()
    {
        base.Awake();
        monsterAnimator = GetComponent<Animator>();
        monsterAudioSource = GetComponent<AudioSource>();
        monsterRigidbody = GetComponent<Rigidbody2D>();
        currentDirection = 0;
    }

    protected void FixedUpdate() // Monster moving
    {
        float distance = Vector2.Distance(transform.position, targetTransform.position);

        if (aliveTarget && !Dead)
        {
            if(distance <= targetLockOnDistance)
            {
                if(!isAttacking)
                    ChaseTarget();
                else
                {
                    monsterRigidbody.velocity = Vector2.zero;
                    SetMonsterIdle();
                }

                if (targetTransform.position.x <= transform.position.x && facingRight
                    || targetTransform.position.x >= transform.position.x && !facingRight) // Flip monster
                {
                    FlipMonster();
                }
            }
            else
            {
                if (Time.time >= nextMoveTime)//이동이 끝난 시점에 새롭게 방향 결정
                {
                    currentDirection = UnityEngine.Random.Range(-1, 2);
                    nextMoveTime = Time.time + randomMoveInterval;
                }
                //이동이 끝나지 않으면 해당 방향 이동 유지
                MoveInDirection(currentDirection);
            }
        }
        else//When player is die or Monster is die;
        {
            monsterRigidbody.velocity = Vector2.zero;
        }
    }
    
    public virtual void Setup(MonsterData monsterData)
    {
        startingHealth = monsterData.health;
        Health = monsterData.health;
        damage = monsterData.damage;
        attackDelay = monsterData.attackDelay;
        speed = monsterData.speed;
        targetLockOnDistance = monsterData.targetLockOnDistance;
        criticalProbability = monsterData.criticalProbability;
    }
    
    public void ChaseTarget()//플레이어가 인식범위 내에 있을때 이동
    {
        Vector2 direction = (targetTransform.position - transform.position).normalized; // Calculate direction towards player
        Vector2 velocity = direction * speed;
        monsterRigidbody.velocity = new Vector2(velocity.x, monsterRigidbody.velocity.y); // Apply velocity to Rigidbody2D
        SetMonsterMove();
    }

    public void FlipMonster()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;
        attackBox.Flip();
        criticalBox.Flip();
    }

    protected void MoveInDirection(int direction)//플레이어가 인식 범위에 없을때 랜덤 이동
    {
        monsterRigidbody.velocity = new Vector2(direction * speed, monsterRigidbody.velocity.y);

        if (direction == 0)
        {
            SetMonsterIdle();
        }
        else if (direction == 1 && !facingRight)
        {
            FlipMonster();
            SetMonsterMove();
        }
        else if (direction == -1 && facingRight)
        {
            FlipMonster();
            SetMonsterMove();
        }
        else
        {
            SetMonsterMove();
        }   
    }
    
    public virtual void BodyAttack()
    {
        if (Time.time >= lastAttackTime + attackDelay)
        {
            targetEntity.OnDamage(damage);
            lastAttackTime = Time.time;
            Debug.Log(("몸샷"));
        }
    }
    
    public void Attack()
    {
        if (!isAttacking && Time.time >= lastAttackTime + attackDelay)
        {
            isAttacking = true;
            bool isCritical = probabilityCalculator.GetRandomNum(criticalProbability);
            
            if (isCritical)
            {
                monsterAnimator.SetBool("Critical", true);
                StartCoroutine(SetCriticalMotion());
            }
            else
            {
                monsterAnimator.SetBool("Attack", true);
                StartCoroutine(SetAttackMotion());
            }
        }
    }

    protected void OnDrawGizmos()//cognize range player's position
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetLockOnDistance);
    }

    public override void OnDamage(float damage)
    {
        if (!Dead)
        {
            //effect play
            monsterAudioSource.PlayOneShot(hitSound);
        }
        base.OnDamage(damage);
    }

    protected override void Die()
    {
        base.Die();
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "GroundSensor")
                continue;
            collider.enabled = false;
        }
        monsterAudioSource.PlayOneShot(deathSound);
        monsterAnimator.SetTrigger("Die");
        StartCoroutine(SetMonsterDie());
    }
    
    protected virtual IEnumerator SetMonsterDie()
    {
        yield return new WaitForSeconds(deathAnimationDuration);
        InstanceCoin();
        gameObject.SetActive(false);
    }

    protected virtual void InstanceCoin()
    {
        GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
        coin.GetComponent<Coin>().SetCoin(GameManager.Instance.Wave * 10f);
    }

    protected override IEnumerator alphaBlink()
    {
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = monsterHalfA;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = FullA;
    }
    
    protected virtual IEnumerator SetCriticalMotion(){yield break;}
    protected virtual IEnumerator SetAttackMotion(){yield break;}
    protected virtual void SetMonsterMove() { }
    protected virtual void SetMonsterIdle() { }
}
