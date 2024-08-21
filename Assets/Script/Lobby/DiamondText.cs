using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondText : MonoBehaviour
{
    public Text diamondText;

    public void LateUpdate()
    {
        diamondText.text = "Diamond : " + PlayerInfo.Instance.Diamond.ToString();
    }
}
