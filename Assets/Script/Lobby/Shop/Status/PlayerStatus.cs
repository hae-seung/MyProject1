using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{
    public int SpeedLevel { get; private set; } = 0;
    public int HealthLevel { get; private set; } = 0;


    public float HealthMultiplier
    {
        get { return 100f + 50f * HealthLevel; }
    }

    public float SpeedMultiplier
    {
        get { return 10f + 2f * SpeedLevel; }
    }
    
    public void UpgradeSpeed()
    {
        SpeedLevel++;
    }

    public void UpgradeHealth()
    {
        HealthLevel++;
    }

    public void LoadData()
    {
        SpeedLevel = PlayerPrefs.GetInt("SpeedLevel");
        HealthLevel = PlayerPrefs.GetInt("HealthLevel");
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("SpeedLevel", SpeedLevel);
        PlayerPrefs.SetInt("HealthLevel", HealthLevel);
    }
    
}
