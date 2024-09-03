using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ReinforceManager : MonoBehaviour
{
    private WeaponStatus weaponStatus;
    private PlayerStatus playerStatus;
    private string weaponName;

    private Text priceText;
    private Text levelText;
    
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
                
                SetChildText(clickedButton);
                priceText.text = "Diamond:" + 10 * weaponStatus.RangeLevel;
                levelText.text = "Lv." + weaponStatus.RangeLevel;
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
                PlayerInfo.Instance.SaveWeaponData();
                
                SetChildText(clickedButton);
                priceText.text = "Diamond:" + 10 * weaponStatus.DamageLevel;
                levelText.text = "Lv." + weaponStatus.DamageLevel;
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
                
                SetChildText(clickedButton);
                priceText.text = "Diamond:" + 10 * weaponStatus.AmmoLevel;
                levelText.text = "Lv." + weaponStatus.AmmoLevel;
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
                
                SetChildText(clickedButton);
                priceText.text = "Diamond:" + 10 * playerStatus.SpeedLevel;
                levelText.text = "Lv." + playerStatus.SpeedLevel;
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
        if (playerStatus != null && playerStatus.HealthLevel < 5)
        {
            int price = playerStatus.HealthLevel > 0 ? 10 * playerStatus.HealthLevel : 10;//0렙일땐 10원
            if (PlayerInfo.Instance.Diamond >= price)
            {
                PlayerInfo.Instance.Diamond -= price;
                playerStatus.UpgradeHealth();
                PlayerInfo.Instance.SavePlayerData();
                
                SetChildText(clickedButton);
                priceText.text = "Diamond:" + 10 * playerStatus.HealthLevel;
                levelText.text = "Lv." + playerStatus.HealthLevel;
            }
        }
        
        if (playerStatus.HealthLevel >= 5)
        {
            clickedButton.interactable = false;
        }
    }


    private void SetChildText(Button clickedButton)
    {
        foreach (Transform child in clickedButton.transform)
        {
            if (child.name == "Price")
            {
                priceText = child.GetComponent<Text>();
            }
            else if (child.name == "Level")
            {
                levelText = child.GetComponent<Text>();
            }
        }
    }
}
