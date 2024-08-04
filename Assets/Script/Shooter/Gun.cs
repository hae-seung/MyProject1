using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
   public enum State
   {
        Ready,
        Empty,
        Reloading
   }

    public State state { get; set; }

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform fireTransform;
    [SerializeField]
    private float timeBetFire;

    public Transform FireTransform
    {
        get
        {
            return fireTransform;
        }
    }
    public GameObject BulletPrefab { 
        get
        {
            return bulletPrefab;
        }
    }

    protected int magCapacity = 30;
    protected int ammoRemain = 100;
    protected int magAmmo;
    protected float reloadTime = 1.2f;
    protected float BulletSpeed = 50f;
    protected float lastFireTime;
    protected void Awake()
    {
        lastFireTime = 0;
        ammoRemain = 100;
        magAmmo = magCapacity;
        state = State.Ready;
    }

   public virtual bool Fire()
   {
        if(state == State.Ready && Time.time >= lastFireTime + timeBetFire)
        {
            lastFireTime = Time.time;
            Shot();
            return true;
        }
        return false;
   }
    protected virtual void Shot()
    { 
        Vector3 shootPosition = fireTransform.position;

     
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mousePosition - shootPosition).normalized;


        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        GameObject bullet = Instantiate(bulletPrefab, shootPosition, rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = shootDirection * BulletSpeed;

        magAmmo--;
        if(magAmmo<=0)
        {
            state = State.Empty;
        }
        Debug.Log("rifle shot");
    }

   public bool reload()
    {
        if(state == State.Reloading || ammoRemain<=0 || magAmmo >= magCapacity)
            return false;
        StartCoroutine(ReloadRoutine());
        return true;
    }
   private IEnumerator ReloadRoutine()
   {
        state = State.Reloading;

        AudioManager.Instance.playReload();

        yield return new WaitForSeconds(reloadTime);
        
        int ammoFill = magCapacity - magAmmo;

        Debug.Log("ammoFill : "+ ammoFill);

        if(ammoRemain <= ammoFill)
        {
            ammoFill = ammoRemain;
        }

        magAmmo += ammoFill;
        ammoRemain -= ammoFill;

        state = State.Ready;
   }
}
