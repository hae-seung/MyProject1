using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgrade : MonoBehaviour
{
    //heal
    //speed
    //maxhealth
    //gold?

    private int heal = 50;
    public Text playerText;
    public int level;
    private float price = 50f;
    
    
    public PlayerHealth player;
    public PlayerMovement playerMovement;
    
    public void HealPlayer()
    {
        float fixedPrice = 50f;
        if(GameManager.Instance.Spend(fixedPrice))
        {
            player.RestoreHealth(heal);
            playerText.text = player.Health + "/" + player.MaxHealth;
        }
    }

    public void IncreaseMaxHealth()
    {
        if (level == 0)
            price = 50f;
        else
            price *= 1.25f;
        
        if (level == 5)
        {
            GetComponent<Button>().interactable = false;
        }
        
        if (GameManager.Instance.Spend(price))
        {
            level++;
            player.MaxHealth += 50;
            playerText.text = "+" + player.MaxHealth;
        }
    }

    public void IncreaseSpeed()
    {
        if (level == 0)
            price = 30f;
        else
            price *=1.25f;
        
        if (level == 5)
        {
            GetComponent<Button>().interactable = false;
        }
        
        if (GameManager.Instance.Spend(price))
        {
            level++;
            playerMovement.MoveSpeed += 10f;
            playerText.text = "Lv." + level;
        }
    }
}

