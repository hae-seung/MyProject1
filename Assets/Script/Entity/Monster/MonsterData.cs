using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : ScriptableObject
{
    public float health;
    public float damage;
    public float speed;
    public float attackDelay;
    public float targetLockOnDistance;
    public int criticalProbability;
}
