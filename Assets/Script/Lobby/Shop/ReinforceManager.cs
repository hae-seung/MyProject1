using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReinforceManager : MonoBehaviour
{
    private WeaponStatus weaponStatus;
    private PlayerStatus playerStatus;
    private string weaponName;
    
    public void OnUpgradeRangeButtonClicked(Button clickedButton)
    {
        weaponName = clickedButton.GetComponent<WeaponName>().GetWeaponName();
        weaponStatus = PlayerInfo.Instance.GetWeaponStatus(weaponName);
        
        if (weaponStatus != null && weaponStatus.RangeLevel < 5)
        {
            int price = weaponStatus.RangeLevel > 0 ? 10 * weaponStatus.RangeLevel : 10;//0렙일땐 10원
            if (PlayerInfo.Instance.Diamond >= price)
            {
                PlayerInfo.Instance.Diamond -= price;
                weaponStatus.UpgradeRange();
                PlayerInfo.Instance.SaveWeaponData();
            }
        }

        if (weaponStatus.RangeLevel >= 5)
        {
            clickedButton.interactable = false;
        }
    }

    public void OnUpgradeDamageButtonClicked(Button clickedButton)
    {
        weaponName = clickedButton.GetComponent<WeaponName>().GetWeaponName();
        weaponStatus = PlayerInfo.Instance.GetWeaponStatus(weaponName);
        
        if (weaponStatus != null && weaponStatus.DamageLevel < 5)
        {
            int price = weaponStatus.DamageLevel > 0 ? 10 * weaponStatus.DamageLevel : 10;//0렙일땐 10원
            if (PlayerInfo.Instance.Diamond >= price)
            {
                PlayerInfo.Instance.Diamond -= price;
                weaponStatus.UpgradeDamage();
                Debug.Log(weaponStatus.DamageLevel);
                PlayerInfo.Instance.SaveWeaponData();
            }
        }

        if (weaponStatus.DamageLevel >= 5)
        {
            clickedButton.interactable = false;
        }
    }

    public void OnUpgradeAmmoButtonClicked(Button clickedButton)
    {
        weaponName = clickedButton.GetComponent<WeaponName>().GetWeaponName();
        weaponStatus = PlayerInfo.Instance.GetWeaponStatus(weaponName);
        
        if (weaponStatus != null && weaponStatus.AmmoLevel < 5)
        {
            int price = weaponStatus.AmmoLevel > 0 ? 10 * weaponStatus.AmmoLevel : 10;//0렙일땐 10원
            if (PlayerInfo.Instance.Diamond >= price)
            {
                PlayerInfo.Instance.Diamond -= price;
                weaponStatus.UpgradeAmmo();
                PlayerInfo.Instance.SaveWeaponData();
            }
        }

        if (weaponStatus.AmmoLevel >= 5)
        {
            clickedButton.interactable = false;
        }
    }

    public void OnUpgradeSpeedButtonClicked(Button clickedButton)
    {
        playerStatus = PlayerInfo.Instance.GetPlayerStatus();
        if (playerStatus != null && playerStatus.SpeedLevel < 5)
        {
            int price = playerStatus.SpeedLevel > 0 ? 10 * playerStatus.SpeedLevel : 10;//0렙일땐 10원
            if (PlayerInfo.Instance.Diamond >= price)
            {
                PlayerInfo.Instance.Diamond -= price;
                playerStatus.UpgradeSpeed();
                PlayerInfo.Instance.SavePlayerData();
            }
        }
        
        if (playerStatus.SpeedLevel >= 5)
        {
            clickedButton.interactable = false;
        }
        
    }

    public void OnUpgradeHealthButtonClicked(Button clickedButton)
    {
        playerStatus = PlayerInfo.Instance.GetPlayerStatus();
        if (playerStatus != null && playerStatus.SpeedLevel < 5)
        {
            int price = playerStatus.HealthLevel > 0 ? 10 * playerStatus.HealthLevel : 10;//0렙일땐 10원
            if (PlayerInfo.Instance.Diamond >= price)
            {
                PlayerInfo.Instance.Diamond -= price;
                playerStatus.UpgradeHealth();
                PlayerInfo.Instance.SavePlayerData();
            }
        }
        
        if (playerStatus.HealthLevel >= 5)
        {
            clickedButton.interactable = false;
        }
    }
}
