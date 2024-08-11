using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    public event Action UsedShop;
    
    private void Start()
    {
        
    }

    public void OpenShop()
    {
        Time.timeScale = 0;//게임 일시정지
    }

    private void ExitShop()//button 활성화 필요
    {
        Time.timeScale = 1;
        UsedShop?.Invoke();
    }
    
}
