using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class MonsterAI : LivingEntity
{
    public Transform targetTransform;
    public LivingEntity targetEntity;
    
    //scriptable data => temp
    protected float targetLockOnDistance = 20f;
    protected float speed = 2f;
    protected float damage = 10f; 
    protected int criticalProbability = 1;
    public float attackDelay; //공격이 끝나고 그 다음 공격까지 시간
    
    //Inheritance
    protected float criticalDamage;
    protected float bodyDamage;
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
    protected SpriteRenderer spriteRenderer;
    
    [SerializeField] protected DrawGizmos attackBox;
    
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
    
    
    protected virtual void Awake()
    {
        monsterAnimator = GetComponent<Animator>();
        monsterAudioSource = GetComponent<AudioSource>();
        monsterRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentDirection = 0;
    }

    protected void FixedUpdate() // Monster moving
    {
        float distance = Vector2.Distance(transform.position, targetTransform.position);

        if (aliveTarget)
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
        else//When player is die;
        {
            return;
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
    
    public void BodyAttack()
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
            bool isCritical = ProbabilityCalculator.Instance.GetRandomNum(criticalProbability);
            
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

    protected void OnDrawGizmos()//cognize plyaer range
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetLockOnDistance);
    }
    protected virtual IEnumerator SetCriticalMotion(){yield break;}
    protected virtual IEnumerator SetAttackMotion(){yield break;}
    protected virtual void SetMonsterMove() { }
    protected virtual void SetMonsterIdle() { }
}
