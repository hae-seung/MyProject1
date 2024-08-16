using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IItem
{
    private float price = 0;
    private float maxDuration = 20f;
    private float curTime = 0.0f; // 현재 경과 시간 초기화

    public void Use(GameObject target)
    {
        GameManager.Instance.AddMoney(price);
        Destroy(gameObject);
    }

    public void SetCoin(float coinPrice)
    {
        price = coinPrice;
    }

    private void Update()
    {
        curTime += Time.deltaTime; // 경과 시간 증가

        if (curTime >= maxDuration)
        {
            Destroy(gameObject);
        }
    }
}
