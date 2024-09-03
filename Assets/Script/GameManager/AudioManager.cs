using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MySingleton<AudioManager>
{
    public AudioClip shotClip;
    public AudioClip jumpClip;
   
    public AudioClip deathClip;
    public AudioClip reloadClip;
    public AudioClip changeClip;
    public AudioClip healClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void playShot()
    {
        audioSource.PlayOneShot(shotClip);
    }
    public void playJump()
    {
        audioSource.PlayOneShot(jumpClip);
    }
    public void playDeath()
    {
        audioSource.PlayOneShot(deathClip);
    }

    public void playReload()
    {
        audioSource.PlayOneShot(reloadClip);
    }

    public void playChangeGun()
    {
        audioSource.PlayOneShot(changeClip);
    }

    public void playHeal()
    {
        audioSource.PlayOneShot(healClip);
    }
}
