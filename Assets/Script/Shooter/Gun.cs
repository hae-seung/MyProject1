using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] private Transform fireTransform;
    
    protected float lastFireTime;
    public Animator playerAnimator;
    
    public string GunName { get; protected set; }
    protected int MagCapacity { get; set; }
    public int AmmoCapacity { get; set; }
    public int MagAmmo { get;  set; }
    protected float ReloadTime { get; set; }
    protected float BulletSpeed { get; set; }
    protected float TimeBetFire { get; set; }
    public float BulletDamage { get; set; }
    public float BulletMaxDistance { get; set; }
    
    
   public enum State
   {
        Ready,
        Empty,
        Reloading,
        Zoom
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
        if((state == State.Ready || state == State.Zoom) && Time.time >= lastFireTime + TimeBetFire)
        {
            lastFireTime = Time.time;
            Shot();
            UIManager.Instance.UpdateAmmoText(MagAmmo, AmmoCapacity);
            return true;
        }
        return false;
   }

    public void Zoom()
    {
        if (state == State.Zoom || state == State.Empty)//줌 해제
        {
            CameraController.Instance.SwitchCamera(false);
            state = State.Ready;
        }
        else if (state == State.Ready)//줌인
        {
            CameraController.Instance.SwitchCamera(true);
            state = State.Zoom;
        }
    }
   
   public bool Reload()//public 고정
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
        
        playerAnimator.SetBool("reload", true);
        yield return new WaitForSeconds(ReloadTime);
        playerAnimator.SetBool("reload", false);
        
        int ammoFill = MagCapacity - MagAmmo;

        Debug.Log("ammoFill : "+ ammoFill);

        if(AmmoCapacity <= ammoFill)
        {
            ammoFill = AmmoCapacity;
        }

        MagAmmo += ammoFill;
        AmmoCapacity -= ammoFill;
       
        UIManager.Instance.UpdateAmmoText(MagAmmo, AmmoCapacity);
        
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
