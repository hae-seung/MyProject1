using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTab : MonoBehaviour
{
    public GameObject[] tabPanelSet;
   
    public void TabClick(int index)
    {
        for (int i = 0; i < tabPanelSet.Length; i++)
        {
            if (tabPanelSet[i].activeSelf && i != index)
            {
                tabPanelSet[i].SetActive(false);
            }
        }
        tabPanelSet[index].SetActive(true);
    }

    public void ClickResume()
    {
        transform.parent.gameObject.SetActive(false);
        GameManager.Instance.Resume();
    }
}