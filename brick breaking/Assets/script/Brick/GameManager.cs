using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Gameplay")]
    public GameObject ballPrefab;
    public Transform paddleTransform;
    [SerializeField] private int startingLives = 3;
    [SerializeField] private GameObject brickPrefab;

    [Header("UI")]
    [SerializeField] private GameObject lifePaddlePrefab;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshPro scoreText;
    [SerializeField] private TextMeshPro niveauText;
    [SerializeField] private TextMeshProUGUI messageFinText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI scoreFinalText;
    [SerializeField] private TextMeshProUGUI relancerText;

    [Header("Audio")]
    public AudioClip hitSound;
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip powerupSound;

    [Header("Bonus")]
    public GameObject bonusPrefab;
    [SerializeField] private GameObject giantPaddle;
    private float enlargeDuration = 5f;
    public List<BrickType> brickTypes = new List<BrickType>();

    private AudioSource audioSource;
    private Coroutine enlargeCoroutine;

    private List<GameObject> lifePaddles = new();
    public int niveau;
    private int activeBalls = 0;
    private int bestScore;
    private int currentLives;
    private int bricksRemaining;
    public int score { get; private set; }

    private float spacingLife = 1.7f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        score = 0;
        gameOverPanel.SetActive(false);

        LoadSavedData();
        InitLifeIcons();
        UpdateUI();
        SpawnBall();
    }

    private void LoadSavedData()
    {
        niveau = PlayerPrefs.GetInt("niveau", 1);
        score = PlayerPrefs.GetInt("score", 0);
        currentLives = PlayerPrefs.GetInt("vie", startingLives);
    }

    private void UpdateUI()
    {
        scoreText.text = $"Score : {score}";
        niveauText.text = $"Niveau : {niveau}";
    }

    private void InitLifeIcons()
    {
        foreach (var icon in lifePaddles)
            Destroy(icon);

        lifePaddles.Clear();

        Vector2 startPos = new Vector2(18.1f, 10.2f);
        for (int i = 0; i < currentLives; i++)
        {
            Vector2 pos = startPos + new Vector2(-i * spacingLife, 0);
            GameObject newLife = Instantiate(lifePaddlePrefab, pos, Quaternion.identity);
            lifePaddles.Add(newLife);
        }
    }

    public void GenerateGrid()
    {
        int rows = 5;
        int cols = 14;
        float spacingX = 2f;
        float spacingY = 1.1f;
        Vector2 startPos = new Vector2(-13.5f, 9.5f);

        float brickSpawnChance = Mathf.Clamp(10 + 5 * niveau, 10, 100);// pourcentage global de chance qu'une brique spawn

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                float chance = Random.Range(0f, 100f);
                if (chance > brickSpawnChance)
                {
                    // pas de brique pour cette case
                    continue;
                }

                float random = Random.Range(0f, 100f);
                float cumulative = 0f;

                foreach (BrickType type in brickTypes)
                {
                    cumulative += type.spawnChance;
                    if (random <= cumulative)
                    {
                        Vector2 pos = startPos + new Vector2(col * spacingX, -row * spacingY);
                        GameObject newBrick = Instantiate(brickPrefab, pos, Quaternion.identity);

                        SpriteRenderer sr = newBrick.GetComponent<SpriteRenderer>();
                        if (sr != null) sr.color = type.color;

                        Brick brick = newBrick.GetComponent<Brick>();
                        if (brick != null)
                        {
                            brick.points = type.points;
                            brick.isBonus = type.isBonus;
                        }

                        break;
                    }
                }
            }
        }
    }


    public void SpawnBall()
    {
        Vector3 spawnPosition = paddleTransform.position + Vector3.up * 0.5f;
        GameObject newBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);

        Ball ballScript = newBall.GetComponent<Ball>();
        if (ballScript != null)
            ballScript.paddle = paddleTransform;
        RegisterBall();
    }

    public void RegisterBrick()
    {
        bricksRemaining++;
    }

    public void DeadBrick(GameObject brick)
    {
        PlaySound(breakSound);
        Destroy(brick);
        bricksRemaining--;
        AddScore(1);

        if (bricksRemaining <= 0)
            Victory();
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = $"Score : {score}";
    }

    public void LoseLife()
    {
        PlaySound(loseSound);
        currentLives--;

        if (currentLives <= 0)
            GameOver();
        else
        {
            InitLifeIcons();
            Invoke(nameof(SpawnBall), 1f);
        }
    }

    public void AddLife()
    {
        if (currentLives < 3)
        {
            currentLives++;
            InitLifeIcons(); 
        }
    }

    public void EnlargePaddle()
    {
        if (enlargeCoroutine != null)
        {
            StopCoroutine(enlargeCoroutine);
        }

        giantPaddle.SetActive(true);
        enlargeCoroutine = StartCoroutine(EnlargeTimer());
    }

    private IEnumerator EnlargeTimer()
    {
        yield return new WaitForSeconds(enlargeDuration);
        giantPaddle.SetActive(false);
        enlargeCoroutine = null;
    }

    public void SpawnMultiball()
    {

        Vector3 spawnPosition = new Vector3(0f, 0f, 0f); // milieu de la map
        for (int i = 0; i < 3; i++)
        {
            GameObject newBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
            Ball ballScript = newBall.GetComponent<Ball>();

            if (ballScript != null)
            {
                ballScript.paddle = paddleTransform; // pour qu'elle suive le paddle avant le lancement
                ballScript.ResetToFollowPaddle(); // réinitialiser son état
                StartCoroutine(AutoLaunchAfterDelay(ballScript, 2f));
            }
            RegisterBall();
        }
    }

    private IEnumerator AutoLaunchAfterDelay(Ball ballScript, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!ballScript.IsLaunched) // tu dois exposer launched en lecture
        {
            ballScript.ForceLaunch();
        }
    }


    public void RegisterBall()
    {
        activeBalls++;
    }

    public void UnregisterBall()
    {
        activeBalls--;

        if (activeBalls <= 0)
        {
            LoseLife();
        }
    }



    public void SpawnBonus(Vector2 position)
    {
        if (bonusPrefab != null)
            Instantiate(bonusPrefab, position, Quaternion.identity);
    }

    private void GameOver()
    {
        if (score > bestScore)
            bestScore = score;

        
        ShowFinalScreen(false);
        score = 0;
        niveau = 1;
        currentLives = 3;
        SaveData();
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Victory()
    {
        if (score > bestScore)
            bestScore = score;
        niveau++;
        ShowFinalScreen(true);
        SaveData();
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ShowFinalScreen(bool victory)
    {
        scoreFinalText.text = $"Score : {score}";
        bestScoreText.text = $"Best Score : {bestScore}";

        messageFinText.text = victory ? "Bravo vous avez réussi le niveau" : "Game Over";
        relancerText.text = victory ? "Continuer" : "Relancer";
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        ClearSavedData();
        Application.Quit();
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("niveau", niveau);
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("bestScore", bestScore);
        PlayerPrefs.SetInt("vie", currentLives);
    }

    private void ClearSavedData()
    {
        PlayerPrefs.DeleteKey("niveau");
        PlayerPrefs.DeleteKey("score");
        PlayerPrefs.DeleteKey("vie");
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
    
}
