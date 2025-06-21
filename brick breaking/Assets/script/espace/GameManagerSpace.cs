using UnityEngine;

public class GameManagerSpace : MonoBehaviour
{
    // Singleton accessible depuis d'autres scripts
    public static GameManagerSpace Instance;

    // Exemples de données de jeu
    private int playerLives = 3;
    private int score = 0;
    
    [Header("Projectile")]
    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private GameObject enemyBulletPrefab;

    void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Assure qu'il n'y ait qu'une seule instance
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persiste entre les scènes
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
    }

    public void RemoveLife()
    {
        playerLives--;
        Debug.Log("Vies restantes : " + playerLives);

        if (playerLives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
        // Tu peux ici charger une scène de fin, désactiver les contrôles, etc.
    }

    public void FirePlayerBullets(Transform firePoint)
    { 
        SpawnBullet(playerBulletPrefab, firePoint.position + new Vector3(0, 0.3f, 0), Vector2.right);
        SpawnBullet(playerBulletPrefab, firePoint.position + new Vector3(0, -0.3f, 0), Vector2.right);

    }

    public void FireEnemyBullets(Transform firePoint)
    {
        SpawnBullet(enemyBulletPrefab, firePoint.position + new Vector3(0, 0.5f, 0), new Vector2(-1f, 0.3f));
        SpawnBullet(enemyBulletPrefab, firePoint.position + new Vector3(0, -0.5f, 0), new Vector2(-1f, -0.3f));
    }

    private void SpawnBullet(GameObject prefab, Vector3 position, Vector2 direction)
    {
        GameObject bullet = Instantiate(prefab, position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = direction * 10f;  // vitesse fixe ou variable
    }
}