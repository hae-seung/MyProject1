using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    protected override void Awake()
    {
        base.Awake();
        MagCapacity = 30;
        AmmoCapacity = 90;
        MagAmmo = MagCapacity;
        ReloadTime = 1.2f;
        TimeBetFire = 0.1f;
        BulletSpeed = 50f;
        BulletDamage = 10f;
        BulletMaxDistance = 20f;
    }
    
    /*
    protected int magCapacity;//한 개의 탄창량//아이템(대용량탄창 업그레이드)
    protected int ammoCapacity;//모든 총탄량//아이템(최대 소지수 증가)
    protected int magAmmo;//현재 탄창에 남은 총알//아이템(총알 보충)
    protected float reloadTime;//재장전 시간//아이템(재장전속도 증가)
    protected float timeBetFire;//총 발사 간격 감소//아이템(스나용)
    protected float BulletSpeed;//총알 날아가는 속도//아이템(총알 속도 증가)
    */

    protected override void Shot()
    {
        Vector3 shootPosition = FireTransform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mousePosition - shootPosition).normalized;
        
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        GameObject bullet = Instantiate(bulletPrefab, shootPosition, rotation);
        
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        
        bullet.GetComponent<Bullet>().SetAttribute(BulletDamage, BulletMaxDistance);//총알 설정
        
        bulletRb.velocity = shootDirection * BulletSpeed;

        MagAmmo--;
        if(MagAmmo<=0)
        {
            state = State.Empty;
        }
        Debug.Log("rifle shot");
    }
}
