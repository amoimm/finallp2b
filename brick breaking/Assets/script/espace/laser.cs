using UnityEngine;

public class laser : MonoBehaviour
{
    public float targetHeight = 5f;
    public float growSpeed = 0.5f;

    private BoxCollider2D boxCollider;
    private bool playerInside = false;
    private float damageTimer = 0f;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // Croissance du laser
        if (transform.localScale.y < targetHeight)
        {
            float newY = Mathf.Min(transform.localScale.y + growSpeed * Time.deltaTime, targetHeight);
            transform.localScale = new Vector3(transform.localScale.x, newY, transform.localScale.z);

            if (boxCollider != null)
            {
                boxCollider.size = new Vector2(boxCollider.size.x, newY);
                boxCollider.offset = new Vector2(0f, newY / 2f);
            }
        }

        // Dégâts toutes les secondes
        if (playerInside)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= 0.7f)
            {
                GameManagerSpace.Instance.LessLive(); // Applique 1 dégât
                damageTimer = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManagerSpace.Instance.LessLive();
            playerInside = true;
            damageTimer = 0f; // Réinitialise le timer à l'entrée
        }
        else if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}