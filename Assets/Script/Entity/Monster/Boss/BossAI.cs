using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonsterAI
{
    protected override IEnumerator SetMonsterDie()
    {
        yield return new WaitForSeconds(deathAnimationDuration);
        Destroy(gameObject);
    }
    
}
