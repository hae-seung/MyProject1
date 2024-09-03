using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : Singleton<PlayerInfo>
{
    private Dictionary<string, WeaponStatus> weaponStatus = new Dictionary<string, WeaponStatus>();
    private PlayerStatus playerStatus;
    
    public int Diamond
    {
        get
        {
           return PlayerPrefs.GetInt("Diamond");
        }
        set
        {
            PlayerPrefs.SetInt("Diamond", value);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        //
        playerStatus = new PlayerStatus();
        weaponStatus.Add("Rifle", new WeaponStatus(WeaponType.Rifle));
        weaponStatus.Add("ShotGun", new WeaponStatus(WeaponType.ShotGun));
        weaponStatus.Add("Snifer", new WeaponStatus(WeaponType.Snifer));

        LoadDataAll();
        SetDiamond();
    }

    private void SetDiamond()
    {
        if (!PlayerPrefs.HasKey("Diamond"))
        {
            PlayerPrefs.SetInt("Diamond", 10000);
            PlayerPrefs.Save();
        }
    }
    
    public void SaveWeaponData()
    {
        foreach (var weapon in weaponStatus)
        {
            weapon.Value.SaveData(weapon.Key);
        }
    }

    public void SavePlayerData()
    {
        playerStatus.SaveData();
    }
    
    private void LoadDataAll()
    {
        foreach (var weapon in weaponStatus)
        {
            weapon.Value.LoadData(weapon.Key);
        }
        playerStatus.LoadData();
    }

    public WeaponStatus GetWeaponStatus(string weaponName)
    {
        foreach (var weapon in weaponStatus)
        {
            if (weaponName == weapon.Key)
            {
                return weapon.Value;
            }
        }
        return null;
    }
    
    public PlayerStatus GetPlayerStatus()
    {
        return playerStatus;
    }
   
}
