using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class DiamondText : MonoBehaviour
{
    public Text diaText;
    private int dia;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Diamond"))
        {
            diaText.text = "Diamond : 0";
        }
        else
        {
            SetDiamondText();
        }
    }
    
    private void LateUpdate()
    {
        SetDiamondText();
    }

    private void SetDiamondText()
    {
        dia = PlayerInfo.Instance.Diamond;
        diaText.text = "Diamond : " + dia;
    }
}
