using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] monsterPrefab;
    public List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[monsterPrefab.Length]; // 배열 초기화

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>(); // List 초기화
        }
    }

    public GameObject GetPrefab(int index)
    {
        GameObject select = null;
        
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.transform.rotation = monsterPrefab[index].transform.rotation; // 회전값 맞추기
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(monsterPrefab[index], transform);
            select.transform.rotation = monsterPrefab[index].transform.rotation; // 회전값 맞추기
            pools[index].Add(select);
        }

        return select;
    }
}