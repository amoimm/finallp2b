using UnityEngine;

public class EnemyShoot2 : MonoBehaviour
{
    public GameObject impactEffectPrefab;
    private float speed = 2f;           // Vitesse initiale
    private float acceleration = 4f;    // Accélération par seconde
    private float currentSpeed;
    
    private void Start()
    {
        currentSpeed = speed;
        transform.rotation = Quaternion.Euler(0f, 0f, 180f); // Rotation visuelle
    }

    private void Update()
    {
        // Augmente la vitesse avec le temps
        currentSpeed += acceleration * Time.deltaTime;

        // Déplace vers la gauche dans l'espace monde
        transform.position += Vector3.left * currentSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(impactEffectPrefab, other.transform.position , Quaternion.identity);
            Destroy(gameObject);
            GameManagerSpace.Instance.LessLive();
        }
        else if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
            Instantiate(impactEffectPrefab, other.transform.position , Quaternion.identity);
            Destroy(other.gameObject);

        }
    }
}