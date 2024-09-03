using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    [SerializeField]
    private Slider healthSlider;
    private float maxHealth = 100f;//player
    public AudioClip deathClip;
    public AudioClip hitClip;
    public AudioClip itemPickupClip;

    private AudioSource playerAudioPlayer;
    private Animator playerAnimator;
    private bool isHurt;
    
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerShooter playerShooter;


    public float MaxHealth {
        get {return maxHealth;}
        set
        {
            maxHealth = value;
            healthSlider.maxValue = value;
        }
    }
    
    protected override void Awake()
    {
        base.Awake();
        playerAnimator = GetComponent<Animator>();
        playerAudioPlayer = GetComponent<AudioSource>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = startingHealth;
        healthSlider.value = Health;

        playerInput.enabled = true;
        playerShooter.enabled = true;
    }

    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
        healthSlider.value = Health;
        if (Health >= maxHealth)
        {
            Health = maxHealth;
        }
    }

    public override void OnDamage(float damage)
    {
        if (!isHurt)
        {
            isHurt = true;
            if (!Dead)
            {
                playerAudioPlayer.PlayOneShot(hitClip);
            }

            base.OnDamage(damage);
            StartCoroutine(HurtRoutine());
            healthSlider.value = Health;
            //play hit sound}
        }
    }

    protected override void Die()
    {
        base.Die();
        healthSlider.gameObject.SetActive(false);
        playerAudioPlayer.PlayOneShot(deathClip);
        //playerAnimator.SetTrigger("Die");
        playerInput.enabled = false;
        playerShooter.enabled = false;
    }

    private IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        isHurt = false;
    }
    
    protected override IEnumerator alphaBlink()
    {
        while(isHurt)
        {
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = playerHalfA;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = FullA;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Dead)
        {
            IItem item = other.GetComponent<IItem>();
            if (item != null)
            {
                item.Use(gameObject);
                //playerAudioPlayer.PlayOneShot(itemPickupClip);
            }
        }
    }
}
