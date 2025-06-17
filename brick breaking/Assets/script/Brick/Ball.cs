using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector2 launchForce = new Vector2(2f, 8f);
    public Transform paddle;
    private Rigidbody2D rb;
    private bool launched = false;
    private float baseSpeed =9f;
    private float minSpeed = 9f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        
        if (paddle == null && GameManager.Instance != null)
        {
            paddle = GameManager.Instance.paddleTransform;
        }
    }
    
    public bool IsLaunched => launched;
    
    public void ForceLaunch()
    {
        if (!launched)
        {
            Launch();
        }
    }

    public void ResetToFollowPaddle()
    {
        launched = false;
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector2.zero;
        }
    }

    void Update()
    {
        if (!launched && paddle != null)
        {
            // Positionner la balle au-dessus du paddle
            transform.position = paddle.position + new Vector3(0, 0.5f, 0);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Launch();
            }
        }
    }

    private void Launch()
    {
        launched = true;
        rb.isKinematic = false;

        int niveau = GameManager.Instance != null ? PlayerPrefs.GetInt("niveau", 1) : 1;
        float speedMultiplier = 1f + (niveau - 1) * 0.1f; // +10% de vitesse par niveau
        float finalSpeed = baseSpeed * speedMultiplier;

        float randomX = Random.Range(-1f, 1f);
        Vector2 dir = new Vector2(randomX, 1f).normalized;

        rb.linearVelocity = dir * finalSpeed;

        GameManager.Instance?.PlaySound(GameManager.Instance.hitSound);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance?.PlaySound(GameManager.Instance.hitSound);
        
        if (!launched && collision.gameObject.CompareTag("Paddle"))
        {
            // Gestion de l’angle de rebond
            float paddleX = collision.transform.position.x;
            float contactX = transform.position.x;
            float offset = (contactX - paddleX) / (collision.collider.bounds.size.x / 2f);
            Vector2 dir = new Vector2(offset, 1f);
            dir = ClampDirection(dir); // ici on corrige l'angle
            rb.linearVelocity = dir * rb.linearVelocity.magnitude;
        }
        else if (collision.gameObject.CompareTag("DeadZone"))
        {
            GameManager.Instance?.UnregisterBall();
            Destroy(gameObject);
        }
        else
        {
            Vector2 newDir = ClampDirection(rb.linearVelocity.normalized);
            float newSpeed = Mathf.Max(rb.linearVelocity.magnitude, minSpeed);
            rb.linearVelocity = newDir * newSpeed;
        }
    }
    private Vector2 ClampDirection(Vector2 direction)
    {
        float minAngle = 20f; // en degrés
        float angle = Vector2.Angle(direction, Vector2.up);

        // Si trop vertical (proche de 0 ou 180), forcer un angle minimum
        if (angle < minAngle || angle > 180 - minAngle)
        {
            direction.x += (direction.x >= 0) ? 0.3f : -0.3f;
        }

        // Si trop horizontal (proche de 90), corriger aussi
        if (angle > 90 - minAngle && angle < 90 + minAngle)
        {
            direction.y += (direction.y >= 0) ? 0.3f : -0.3f;
        }

        return direction.normalized;
    }


}