using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class PlayererInput : MonoBehaviour
{
    public Transform firepos;
    public Slider healthSlider;  // 체력 슬라이더

    private bool facingRight = true;
    public float Axis { get; private set; }
    public bool Jump { get; private set; }
    public bool Shot { get; private set; }
    public bool Reload { get; private set; }
    public bool RifleGun { get; private set; }
    public bool ShotGun { get; private set; }

    private void Update()
    {
        RifleGun = Input.GetKeyDown(KeyCode.Alpha1);
        ShotGun = Input.GetKeyDown(KeyCode.Alpha2);
    }

    private void FixedUpdate()
    {
        Axis = Input.GetAxis("Horizontal");
        Jump = Input.GetKey(KeyCode.Space);
        Shot = Input.GetMouseButton(0);
        Reload = Input.GetKey(KeyCode.R);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x < firepos.position.x - 1.75f / 2 && facingRight)
        {
            FlipCharacter();
        }
        else if (mousePosition.x >= firepos.position.x + 1.75f / 2 && !facingRight)
        {
            FlipCharacter();
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        NormalizeHealthSlider();
    }

    private void NormalizeHealthSlider()
    {
        Vector3 healthScale = healthSlider.transform.localScale;
        healthScale.x *= -1;
        healthSlider.transform.localScale = healthScale;
    }
    
    
}