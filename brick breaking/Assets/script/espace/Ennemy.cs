using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public int points;
    private SpriteRenderer spriteRenderer;
    private int lives;

    private float stopX = 7f;
    private float speed = 2f; // Ajuste la vitesse selon tes besoins
    private Rigidbody2D rb;

    // Variables to handle the enemy1 dash behavior
    private bool waiting = false;
    private bool isDashing = false;
    private float waitDuration = 1.0f;
    private float waitTimer = 0f;
    private float dashSpeed = -8f;
    //Variables to handle the enemy2 shooting behavior
    [SerializeField] private GameObject missilePrefab;
    private float missileCooldown = 1f;
    private float missileTimer = 0f;
    // Variables to handle the enemy3 laser behavior
    [SerializeField] private GameObject laserPrefab;
    private float laserCooldown = 7.5f;
    private float laserTimer = 0f;
    private float freezeAfterLaser = 2.5f; // Durée d'immobilisation après tir du laser
    private float freezeTimer = 0f;
    private bool needNewTargetY = true;
    private float targetY;

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// Setup is called to initialize the ennemy with a sprite and points value
    /// </summary>
    public void Setup(Sprite sprite, int pointsValue, int livesValue)
    {
        spriteRenderer.sprite = sprite;
        points = pointsValue;
        lives = livesValue;
    }

    /// <summary>
    /// Update is called once per frame, this is where the movement logic is handled
    /// if its ennemy1, it will wait for a while before dashing to the left
    /// Otherwise, it will move to the left until it reaches stopX and shoot bullets
    /// </summary>
    void Update()
    {
        // Move the ennemy to the left until it reaches stopX
        if (transform.position.x > stopX)
        {
            rb.linearVelocity = new Vector2(-speed, 0f);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;

            switch (points)
            {
                case 300:
                    // Fire missiles for ennemy2
                    FireMissiles();
                    break;
                case 500:
                    // Fire lasers for ennemy3
                    MoveRandomlyVertical();
                    FireLaser();
                    
                    break;
                default:
                    Dash();
                    break;
            }
        }
    }

    // --- Ennemy1 behavior ---
    void Dash()
    {
        if (!waiting && !isDashing)
        {
            waiting = true;
            waitTimer = waitDuration;
        }
        else
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                rb.linearVelocity = new Vector2(dashSpeed, 0f);
                waiting = false;
                isDashing = true;
            }
        }
    }

    // --- Ennemy2 behavior ---
    void FireMissiles()
    {
        missileTimer -= Time.deltaTime;
        if (missileTimer <= 0f)
        {
            // Randomly generate an angle for the missile
            float angle = Random.Range(-45f, 45f);
            Instantiate(missilePrefab, transform.position, Quaternion.Euler(0f, 0f, angle));
            missileTimer = missileCooldown;
        }
    }

    // --- Ennemy3 behavior ---
    void FireLaser()
    {
        laserTimer -= Time.deltaTime;
        if (laserTimer <= 0f)
        {
            Instantiate(laserPrefab, transform.position + new Vector3(-1.5f, 0f, 0f), Quaternion.identity);
            laserTimer = laserCooldown;
            // Set a new target Y position when the laser is fired
            targetY = Random.Range(-4.5f, 3.2f);
            needNewTargetY = true;
            // Freeze the enemy during the animation of the laser
            freezeTimer = freezeAfterLaser;
        }
    }

    void MoveRandomlyVertical()
    {
        if (freezeTimer > 0f)
        {
            freezeTimer -= Time.deltaTime;
            return;
        }
        // If the laser has just been fired, set a new target Y position
        if (needNewTargetY && Mathf.Abs(transform.position.y - targetY) > 0.05f)
        {
            // Progressively move towards the target Y position
            Vector3 pos = transform.position;
            pos.y = Mathf.MoveTowards(pos.y, targetY, speed * Time.deltaTime);
            transform.position = pos;
        }
        else
        {
            // Target reached, stop moving until next laser shot
            needNewTargetY = false;
        }
        
        
    }
}
