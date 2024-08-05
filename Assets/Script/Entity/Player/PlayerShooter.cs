using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private PlayererInput playerInput;
    private Animator animator;
    private int rifle = 0;
    private int shotgun = 1;
    //private int snifer = 2;
    
    [SerializeField]
    private Gun[] guns;

    private Gun gun;

    private void Start()
    {
        playerInput = GetComponent<PlayererInput>();
        animator = GetComponent<Animator>();
        gun = guns[rifle];
    }
    private void Update()
    {
        if(playerInput.Shot)
        {
           if(gun.Fire())
           {
                animator.SetTrigger("shot");
                AudioManager.Instance.playShot();
           }
        }

        if(playerInput.Reload)
        {
            if(gun.reload())
            {
                animator.SetTrigger("reload");
            }
        }

        if(playerInput.RifleGun)
        {
            SwitchGun(rifle);
            AudioManager.Instance.playChangeGun();
        }

        if(playerInput.ShotGun)
        {
            SwitchGun(shotgun);
            AudioManager.Instance.playChangeGun();
        }
    }

    private void SwitchGun(int gunmode)
    {
        if(gunmode>=0 && gunmode<guns.Length)
        {
            gun = guns[gunmode];
        }
    }
    
}
