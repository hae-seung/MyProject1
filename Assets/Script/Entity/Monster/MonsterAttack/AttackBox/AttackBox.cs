using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    [SerializeField] private Transform pos; // 공격 박스의 위치
    [SerializeField] private Vector2 boxSize; // 공격 박스의 크기
    

    public void AttackPlayer(float damage)
    {
        // pos의 위치를 사용하여 충돌 감지
        Vector2 adjustedPosition = pos.position;

        // 방향에 따라 조정된 위치 설정
        Collider2D[] colliders = Physics2D.OverlapBoxAll(adjustedPosition, boxSize, 0);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log("일반공격!");
                collider.GetComponent<LivingEntity>().OnDamage(damage);
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 adjustedPosition = pos.position;

        // 기즈모의 위치 그리기
        Gizmos.DrawWireCube(adjustedPosition, boxSize);
    }

    public void Flip()
    {
        // pos의 X 위치를 반전하여 방향 전환
        Vector3 position = pos.localPosition;
        position.x = -position.x;
        pos.localPosition = position;
    }
}