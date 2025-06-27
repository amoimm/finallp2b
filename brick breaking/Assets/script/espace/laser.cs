using UnityEngine;

public class laser : MonoBehaviour
{
    public float targetHeight = 3f;
    public float growSpeed = 1f;

    private BoxCollider2D boxCollider;
    private bool playerInside = false;
    private float damageTimer = 0f;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Destroy(gameObject, 2.5f);
    }

    void Update()
    {
        // Grow the laser progressively
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

        // Apply damage every second
        if (playerInside)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= 0.5f)
            {
                GameManagerSpace.Instance.LessLive(); // Apply 1 damage
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
            damageTimer = 0f; // Reset timer at 0
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