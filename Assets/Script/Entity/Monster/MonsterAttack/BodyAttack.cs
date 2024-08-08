using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAttack : MonoBehaviour
{
    [SerializeField] private MonsterAI monster;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            monster.BodyAttack();//몬스터가 데미지를 주는 몸샷 영역
        }
    }
}
