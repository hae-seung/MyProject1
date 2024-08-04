using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilityCalculator : Singleton<ProbabilityCalculator>
{
  
    public bool GetRandomNum(int probability)
    {
        int randNum = Random.Range(1, 101);
            
        if (randNum <= probability)
            return true;
        else
            return false;   
    }
}
