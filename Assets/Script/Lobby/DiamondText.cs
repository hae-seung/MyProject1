using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondText : MonoBehaviour
{
    public Text diamondText;
    public int dia;
    
    private void Awake()
    {
        dia = PlayerPrefs.HasKey("Diamond") ? PlayerPrefs.GetInt("Diamond"): 0;
    }
    
    public void LateUpdate()
    {
        diamondText.text = "Diamond : " + dia;
    }
}
