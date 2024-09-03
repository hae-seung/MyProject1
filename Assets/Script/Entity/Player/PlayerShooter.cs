using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private PlayerInput playerInput;
    private Animator animator;
    private int rifle = 0;
    private int shotgun = 1;
    private int snifer = 2;
    
    [SerializeField]
    private Gun[] guns;

    private Gun currentGun;
    
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        currentGun = guns[rifle];
    }
    private void Update()
    {
        if(playerInput.Shot)
        {
           if(currentGun.Fire())
           {
                animator.SetTrigger("shot");
                AudioManager.Instance.playShot();
           }
        }

        if(playerInput.Reload)
        {
            if(currentGun.reload())
            {
                animator.SetTrigger("reload");
            }
        }

        if (playerInput.SniferZoom)
        {
            if (currentGun == guns[snifer])
                currentGun.Zoom();
        }

        if (playerInput.selectedGun != null)
        {
            SwitchGun(playerInput.selectedGun);
        }
        
    }

    private void SwitchGun(PlayerInput.GunType gunType)
    {
        if(CameraController.Instance.GetZoomStatus() && gunType != PlayerInput.GunType.Snifer)
            CameraController.Instance.SwitchCamera(false);
        
        switch (gunType)
        {
            case PlayerInput.GunType.Rifle:
                currentGun = guns[rifle];
                UIManager.Instance.UpdateGunModeText("Rifle");
                break;
            case PlayerInput.GunType.ShotGun:
                currentGun = guns[shotgun];
                UIManager.Instance.UpdateGunModeText("Shotgun");
                break;
            case PlayerInput.GunType.Snifer:
                if(PlayerPrefs.HasKey("UnlockQuest"))
                {
                    currentGun = guns[snifer];
                    UIManager.Instance.UpdateGunModeText("Sniper");
                }
                break;
        }
        UIManager.Instance.UpdateAmmoText(currentGun.MagAmmo, currentGun.AmmoCapacity);
    }
    
}
