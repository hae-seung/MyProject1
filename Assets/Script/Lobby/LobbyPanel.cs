using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyPanel : MonoBehaviour
{
   public GameObject[] panelSet;
   
   public void StartButtonClicked()
   {
      SceneManager.LoadScene("Main");
   }

   public void ButtonClicked(int index)
   {
      for (int i = 0; i < panelSet.Length; i++)
      {
         if (panelSet[i].activeSelf && i != index)
         {
            panelSet[i].SetActive(false);
         }
      }
      panelSet[index].SetActive(true);
   }

}
