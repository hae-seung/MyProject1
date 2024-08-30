using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponName : MonoBehaviour
{
    [SerializeField] private string weaponName;

    public string GetWeaponName()
    {
        return weaponName;
    }
}
