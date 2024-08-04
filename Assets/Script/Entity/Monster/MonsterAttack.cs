using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField] private MonsterAI monster;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
            monster.Attack();
    }
}
