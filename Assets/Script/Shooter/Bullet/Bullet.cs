using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Vector3 startPosition;
    protected float MaxDistance { get; set; }
    protected float Damage { get; set; }
    
    protected virtual void Start()
    {
        startPosition = transform.position;
    }

    protected void Update()
    {
        float distanceTravelled = Vector3.Distance(startPosition, transform.position);
        
        if (distanceTravelled > MaxDistance)
        {
            Destroy(gameObject);
        }
    }
    
    protected void OnTriggerEnter2D(Collider2D other)
    {
        MonsterAI monsterAI = other.GetComponent<MonsterAI>();
        if (monsterAI != null && !monsterAI.Dead)
        {
            HitMonster(monsterAI);
        }
    }

    protected virtual void HitMonster(MonsterAI monster)
    {
        monster.OnDamage(Damage);
        Destroy(gameObject);
    }

    public void SetAttribute(float bulletdamage, float maxdistance)
    {
        Damage = bulletdamage;
        MaxDistance = maxdistance;
    }
}