using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    
    public float speed = 10f;
    [SerializeField] private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
    }
}