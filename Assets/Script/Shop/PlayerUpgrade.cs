using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgrade : MonoBehaviour
{
    private int heal = 50;
    public Text playerText;
    public Text priceText;
    public int level;
    private float price = 50f;
    
    
    public PlayerHealth player;
    public PlayerMovement playerMovement;
    
    public void HealPlayer()
    {
        float fixedPrice = 50f;
        
        if (player.Health >= player.MaxHealth)
        {
            Debug.Log("강화불가");  
            return;
        }
        
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
            price = Mathf.RoundToInt(price * 1.25f);
        
        if (GameManager.Instance.Spend(price))
        {
            level++;
            player.MaxHealth += 50;
            playerText.text = "+" + player.MaxHealth;
            priceText.text = price + "$";
        }
        
        if (level == 5)
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public void IncreaseSpeed()
    {
        if (level == 0)
            price = 30f;
        else
            price = Mathf.RoundToInt(price * 1.25f);
        
        if (GameManager.Instance.Spend(price))
        {
            level++;
            playerMovement.MoveSpeed += 10f;
            playerText.text = "Lv." + level;
            priceText.text = price + "$";
        }
        
        if (level == 5)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}

