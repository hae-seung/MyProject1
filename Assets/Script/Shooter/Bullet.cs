using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public float maxDistance; 
    private Vector3 startPosition;
    public float bulletDamage = 10f;

    void Start()
    {
        startPosition = transform.position;  
    }

    void Update()
    {
       
        float distanceTravelled = Vector3.Distance(startPosition, transform.position);

       
        if (distanceTravelled > maxDistance)
        {
            Destroy(gameObject);
        }
    }
    
    //if hit monster => onTriggerEnter2D => Damage
}