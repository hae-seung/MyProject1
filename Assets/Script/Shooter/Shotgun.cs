using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Shotgun : Gun
{
    private float lowAngle;
    private float highAngle;

    protected override void Awake()
    {
        base.Awake();
        MagCapacity = 15;
        AmmoCapacity = 60;
        MagAmmo = MagCapacity;
        ReloadTime = 1.2f;
        TimeBetFire = 0.5f;
        BulletSpeed = 100f;
        BulletDamage = 15f;
        BulletMaxDistance = 10f;
    }

    protected override void Shot()
    {
        Vector3 shootPosition = FireTransform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mousePosition - shootPosition).normalized;
        
        float midleAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

        lowAngle = midleAngle - 10f;
        highAngle = midleAngle + 10f;
        float[] angles = { lowAngle, midleAngle, highAngle };

        foreach (float angle in angles)
        {
            // 각도를 라디안으로 변환
            float radianAngle = angle * Mathf.Deg2Rad;

            // 각도를 이용해 방향 벡터를 계산
            Vector2 direction = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle));
            
            // 탄환 생성 및 방향 설정
            GameObject bullet = Instantiate(bulletPrefab, shootPosition, Quaternion.AngleAxis(angle, Vector3.forward));
            
            bullet.GetComponent<Bullet>().SetAttribute(BulletDamage, BulletMaxDistance);//총알 설정
            
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = direction * BulletSpeed;
        }
        
        MagAmmo -= 3;
        if (MagAmmo <= 0)
        {
            state = State.Empty;
        }
        
        Debug.Log("shotgun shot");
    }
}