using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] private Transform fireTransform;
    
    protected float lastFireTime;
    protected int MagCapacity { get; set; }
    protected int AmmoCapacity { get; set; }
    protected int MagAmmo { get; set; }
    protected float ReloadTime { get; set; }
    protected float BulletSpeed { get; set; }
    protected float TimeBetFire { get; set; }
    protected float BulletDamage { get; set; }
    protected float BulletMaxDistance { get; set; }
    
    
   public enum State
   {
        Ready,
        Empty,
        Reloading
   }
   
    public Transform FireTransform
    {
        get
        {
            return fireTransform;
        }
    }
    
    public State state { get; set; }
    
    protected virtual void Awake()
    {
        lastFireTime = 0f;
        state = State.Ready;
    }
    
   public bool Fire()
   {
        if(state == State.Ready && Time.time >= lastFireTime + TimeBetFire)
        {
            lastFireTime = Time.time;
            Shot();
            return true;
        }
        return false;
   }
   
   public bool reload()//public 고정
    {
        if(state == State.Reloading || AmmoCapacity<=0 || MagAmmo >= MagCapacity)
            return false;
        StartCoroutine(ReloadRoutine());
        return true;
    }
   
   protected IEnumerator ReloadRoutine()
   {
        state = State.Reloading;

        AudioManager.Instance.playReload();

        yield return new WaitForSeconds(ReloadTime);
        
        int ammoFill = MagCapacity - MagAmmo;

        Debug.Log("ammoFill : "+ ammoFill);

        if(AmmoCapacity <= ammoFill)
        {
            ammoFill = AmmoCapacity;
        }

        MagAmmo += ammoFill;
        AmmoCapacity -= ammoFill;

        state = State.Ready;
   }
   protected virtual void Shot() {}
   
   protected void AddBulletSpeed(){}
   protected virtual void AddMagCapacity(){}
   protected virtual void AddAmmoCapacity(){}
   protected virtual void AddMagAmmo(){}
   protected virtual void CoolDownReload(){}
   protected virtual void CoolDownShotInterval(){}
   
   /*
    MagCapacity;//한 개의 탄창량//아이템(대용량탄창 업그레이드)
    AmmoCapacity;//모든 총탄량//아이템(최대 소지수 증가)
    MagAmmo;//현재 탄창에 남은 총알//아이템(총알 보충)
    ReloadTime;//재장전 시간//아이템(재장전속도 증가)
    TimeBetFire;//총 발사 간격 감소//아이템(스나용)
    BulletSpeed;//총알 날아가는 속도//아이템(총알 속도 증가)
    */
}
