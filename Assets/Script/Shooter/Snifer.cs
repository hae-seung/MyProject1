using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snifer : Gun
{
    private LineRenderer lineRenderer;
    public float lineDuration = 0.2f; // 라인이 잠시 표시될 시간

    protected override void Awake()
    {
        base.Awake();
        GunName = "sniper";
        MagCapacity = 2;
        AmmoCapacity = 10;
        MagAmmo = MagCapacity;
        ReloadTime = 2.5f;
        TimeBetFire = 2.0f;
        BulletDamage = 200f;
        BulletMaxDistance = Mathf.Infinity;  // 사실상 무한 거리
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;  // 처음엔 라인렌더러 비활성화
    }

    protected override void Shot()
    {
        // 마우스 위치를 월드 좌표로 변환
        Vector3 mouseWorldPosition = CameraController.Instance.GetMouseWorldPosition();
        
        // 총구에서 마우스 위치 방향으로의 벡터 계산
        Vector2 rayDirection = (mouseWorldPosition - FireTransform.position).normalized;

        // 레이캐스트를 발사하여 모든 충돌 물체 가져오기
        RaycastHit2D[] hits = Physics2D.RaycastAll(FireTransform.position, rayDirection, Mathf.Infinity);
        
        // 라인 렌더러 초기화 및 시작점 설정
        lineRenderer.positionCount = 2; // 라인렌더러의 점을 2개로 설정 (시작점과 끝점)
        lineRenderer.SetPosition(0, FireTransform.position); // 시작점: 총구 위치
        Vector3 lineEndPoint = FireTransform.position + (Vector3)rayDirection * 100f; // 기본적으로 매우 먼 거리까지 발사

        // 레이캐스트 충돌 처리
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                MonsterAI monster = hit.collider.GetComponent<MonsterAI>();
                if (monster != null)
                {
                    // 적에게 데미지 적용
                    monster.OnDamage(BulletDamage);
                    Debug.Log("Hit monster: " + hit.collider.name);

                    // 라인렌더러의 끝점을 적의 위치로 설정
                    lineEndPoint = hit.point;
                    break;
                }
                else
                {
                    Debug.Log("Hit object: " + hit.collider.name + ", but it's not a monster.");
                }
            }
        }

        // 라인렌더러의 끝점 설정 (레이캐스트 성공 시 해당 위치까지, 실패 시 마우스 방향으로 긴 거리)
        lineRenderer.SetPosition(1, lineEndPoint);

        // 라인 렌더러를 활성화
        StartCoroutine(ShowLineRenderer());

        // 탄약 감소
        MagAmmo--;
        if (MagAmmo <= 0)
        {
            state = State.Empty;
        }
    }

    // 라인 렌더러를 잠시 표시하고 비활성화하는 코루틴
    private IEnumerator ShowLineRenderer()
    {
        lineRenderer.enabled = true;  // 라인 렌더러 활성화
        yield return new WaitForSeconds(lineDuration);  // 일정 시간 기다림
        lineRenderer.enabled = false;  // 라인 렌더러 비활성화
    }
}
