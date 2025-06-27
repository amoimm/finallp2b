using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private GameObject player;
    private float speed = 5f;
    private float fireCooldown = 0.15f;
    private float fireTimer = 0f;
    [SerializeField] private Transform firePoint; // Le point de départ du tir (souvent un Empty enfant du joueur)

    private float bulletSpeed = 10f;      // Vitesse du projectile
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Entrée clavier
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");


        //Deplacement
        Vector3 direction = new Vector3(x, y, 0f).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Clamp la position du joueur dans la zone autorisée
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -14.2f, -9.0f);
        pos.y = Mathf.Clamp(pos.y, -6.5f, 0.75f);
        transform.position = pos;

        fireTimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && fireTimer <= 0f)
        {
            GameManagerSpace.Instance.FirePlayerBullets(firePoint);
            fireTimer = fireCooldown;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Projectile") || other.gameObject.CompareTag("Enemy"))
        {
            // Handle collision with projectile or enemy
            GameManagerSpace.Instance.LessLive();
            Destroy(other.gameObject);
        }
    }
    
}
