using System;
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

    [SerializeField] private PlayererInput playerInput;
    [SerializeField] private PlayerShooter playerShooter;

    private void Awake()
    {
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
        if (!Dead)
        {
            playerAudioPlayer.PlayOneShot(hitClip);
        }
        base.OnDamage(damage);
        healthSlider.value = Health;
        //play hit sound
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
