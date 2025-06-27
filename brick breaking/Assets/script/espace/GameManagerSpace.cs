using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerSpace : MonoBehaviour
{
    public static GameManagerSpace Instance;

    private int playerLives = 3;
    private int score = 0;
    [SerializeField] private GameObject targetObject; 
    private SpriteRenderer spriteRenderer;

    protected int hitCount = 0;
    private int maxHits = 10;
    [SerializeField] TextMeshPro _scoreText;

    
    [Header("Projectile")]
    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private GameObject enemyBulletPrefab2;
    [SerializeField] private GameObject enemyBulletPrefab3;

    
    private void Start()
    {
        spriteRenderer = targetObject.GetComponent<SpriteRenderer>();

    }

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");
        }
    }

    public void AddScore(int points)
    {
        score += points;
        _scoreText.text = "" + score;
    }

    

    private void GameOver()
    {
        Debug.Log("GAME OVER");
    }

    public void FirePlayerBullets(Transform firePoint)
    { 
        SpawnBullet(playerBulletPrefab, firePoint.position + new Vector3(0, 0.3f, 0), Vector3.right);
        SpawnBullet(playerBulletPrefab, firePoint.position + new Vector3(0, -0.3f, 0), Vector3.right);

    }

    public void FireEnemyBullets1(Transform firePoint)
    {
        SpawnBullet(enemyBulletPrefab, firePoint.position + new Vector3(0, 0.5f, 0), new Vector2(-1f, 0.3f));
        SpawnBullet(enemyBulletPrefab, firePoint.position + new Vector3(0, -0.5f, 0), new Vector2(-1f, -0.3f));
    }
    
    public void FireEnemyBullets2(Transform firePoint)
    {
        SpawnBullet(enemyBulletPrefab2, firePoint.position + new Vector3(0, 0, 0), new Vector2(-1f, 0f));
    }
    
    public void FireEnemyBullets3(Transform firePoint)
    {
        SpawnBullet(enemyBulletPrefab3, firePoint.position , new Vector2(0f, 0f));
    }

    private void SpawnBullet(GameObject prefab, Vector3 position, Vector2 direction)
    {
        GameObject bullet = Instantiate(prefab, position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = direction * 100f;  // vitesse fixe ou variable
    }


    public void LessLive()
    {

        hitCount = Mathf.Min(hitCount + 1, maxHits); // Incrémente sans dépasser maxHits
        Debug.Log(hitCount);
        float t = (float)hitCount / 16; // Valeur entre 0 (vert) et 1 (rouge)
        spriteRenderer.color = Color.Lerp(Color.green, Color.red, t);
    }

}