using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public Text ammoText;
    public Text scoreText;
    public Text waveText;
    public Text moneyText;
    public Text gunModeText;
    public GameObject gameoverUI;
    
    //탄알 텍스트 갱신
    public void UpdateAmmoText(int magAmmo, int remainAmmo)
    {
        ammoText.text = magAmmo + "/" + remainAmmo;
    }

    public void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score : " + newScore;
    }

    public void UpdateWaveText(int waves, int count)
    {
        waveText.text = "Wave : " + waves + "\nEnemy Left : " + count;
    }

    public void UpdateMoneyText(int money)
    {
        moneyText.text = "Money : " + money + "원";
    }

    public void UpdateGunModeText(string gun)
    {
        
        gunModeText.text = gun;
    }

    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameoverUI.SetActive(false);
    }
}
