using UnityEditor;
using UnityEngine;

public enum WeaponType
{
   Rifle,
   ShotGun,
   Snifer
}

public class WeaponStatus
{
   private WeaponType weaponType;

   public WeaponStatus(WeaponType type) //생성자
   {
      weaponType = type;
   }

   public int RangeLevel { get; private set; } = 0;
   public int DamageLevel { get; private set; } = 0;
   public int AmmoLevel { get; private set; } = 0;

   public float RangeMultiplier
   {
      get
      {
         switch (weaponType)
         {
            case WeaponType.Rifle:
               return 1f + 4f * RangeLevel;
            case WeaponType.ShotGun:
               return 1f + 1.5f * RangeLevel;
            default://스나이퍼는 사거리 불필요 확정 타격으로 갈거임
               return 1f;
         }
      }
   }

   public float DamageMultiplier
   {
      get
      {
         switch (weaponType)
         {
            case WeaponType.Rifle:
               return 5f + 5f * DamageLevel;
            case WeaponType.ShotGun:
               return 3f + 6f * DamageLevel;
            default:
               return 1f;
         }
      }
   }

   public float AmmoMultiplier
   {
      get
      {
         switch (weaponType)
         {
            case WeaponType.Rifle:
               return 120 + 30 * AmmoLevel;
            case WeaponType.ShotGun:
               return 90 + 30 * AmmoLevel;
            case WeaponType.Snifer:
               return 1 + 3 * AmmoLevel;
            default:
               return 1f;
         }
      }
   }
   
   public void UpgradeRange()
   {
      RangeLevel++;
   }

   public void UpgradeDamage()
   {
      DamageLevel++;
   }

   public void UpgradeAmmo()
   {
      AmmoLevel++;
   }
   
   public void LoadData(string weaponName)
   {
      RangeLevel = PlayerPrefs.GetInt(weaponName + "RangeLevel", 0);
      DamageLevel = PlayerPrefs.GetInt(weaponName + "DamageLevel", 0);
      AmmoLevel = PlayerPrefs.GetInt(weaponName + "AmmoLevel", 0);
   }

   public void SaveData(string weaponName)
   {
      PlayerPrefs.SetInt(weaponName + "RangeLevel", RangeLevel);
      PlayerPrefs.SetInt(weaponName + "DamageLevel", DamageLevel);
      PlayerPrefs.SetInt(weaponName + "AmmoLevel", AmmoLevel);
   }
}
