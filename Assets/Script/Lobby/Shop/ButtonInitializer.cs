using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInitializer : MonoBehaviour
{
    public GameObject[] reinforceGroups;

    private void Start()
    {
        //배열에서 1개의 그룹을 떼와서
        //그 그룹 내부의 자식들의 버튼을 전부 초기화
        
        for (int index = 0; index < reinforceGroups.Length; index++)
        {
            GameObject reinforceGroup = reinforceGroups[index];//그룹 1개를 들고옴
            
            string groupName = reinforceGroup.name.Replace("Group", "");
            
            foreach (Transform child in reinforceGroup.transform)//그룹 내부의 버튼을 전부 초기화
            {
                Button button = child.GetComponent<Button>();
                if (button != null)
                {
                    InitButton(button, groupName);
                }
            }
        }
    }

    private void InitButton(Button button, string groupName)
    {
        WeaponStatus weaponStatus = null;
        PlayerStatus playerStatus = PlayerInfo.Instance.GetPlayerStatus();

        int level = 0;
        int price = 0;
        
        // 그룹 이름에 따라 상태를 가져옵니다.
        switch (groupName)
        {
            case "Range":
            case "Damage":
            case "Ammo":
                string weaponName = button.GetComponent<WeaponName>().GetWeaponName();
                weaponStatus = PlayerInfo.Instance.GetWeaponStatus(weaponName);

                if (weaponStatus != null)
                {
                    level = groupName switch
                    {
                        "Range" => weaponStatus.RangeLevel,
                        "Damage" => weaponStatus.DamageLevel,
                        "Ammo" => weaponStatus.AmmoLevel,
                        _ => level //default
                    };
                }
                break;

            case "Speed":
                level = playerStatus?.SpeedLevel ?? 0;
                break;

            case "Health":
                level = playerStatus?.HealthLevel ?? 0;
                break;
        }

        if (level >= 5)
            button.interactable = false;
        
        price = level == 0 ? 10 : 10 * level;

        // 버튼의 자식으로 존재하는 텍스트들을 모두 할당하고 업데이트합니다.
        foreach (Transform child in button.transform)
        {
            if (child.name == "Price")
            {
                Text priceText = child.GetComponent<Text>();
                if (priceText != null) priceText.text = "Diamond:" + price;
            }
            else if (child.name == "Level")
            {
                Text levelText = child.GetComponent<Text>();
                if (levelText != null) levelText.text = "Lv." + level;
            }
        }
    }

}
