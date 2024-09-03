using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snifer : Gun
{
    //[SerializeField] protected GameObject bulletPrefab;

    protected override void Awake()
    {
        base.Awake();
        GunName = "sniper";
        MagCapacity = 2;
        AmmoCapacity = 10;
        MagAmmo = MagCapacity;
        ReloadTime = 2.5f;
        TimeBetFire = 2.0f;
        //BulletSpeed
        BulletDamage = 200f;
        //BulletMaxDistance
    }

    protected override void Shot()
    {
        
    }

   
}
