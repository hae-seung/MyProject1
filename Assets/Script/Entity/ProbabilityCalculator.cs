using UnityEngine;

public class ProbabilityCalculator
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
