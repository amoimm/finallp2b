using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject applePrefab;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite goldenSprite;
    [SerializeField] Sprite rottenSprite;
    private float spawnTimer = 1.5f;
    private float timeLeft = 120f;

    /// <summary>
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {
        
    }
    /// <summary>
    /// Update is called once per frame, this is where the spawning logic is handled
    /// </summary>
    void Update()
    {
        // Decrease the time left and spawn timer
        timeLeft -= Time.deltaTime;
        spawnTimer -= Time.deltaTime;

        float minInterval = 0.70f;
        float maxInterval = Mathf.Max(1.5f-Time.time*0.025f, minInterval);
        
        if (spawnTimer <= 0 && timeLeft >= 1f)
        {
            SpawnApple();
            spawnTimer = Random.Range(maxInterval*0.8f, maxInterval*1.2f);
        }
    }

    /// <summary>
    /// allow to spawn apple objects at random positions with different types
    /// and point values based on a random number (1 : normal, 5 : golden, -3 : rotten)
    /// </summary>
    private void SpawnApple()
    {
        int rand = Random.Range(1, 101);
        Sprite chosenSprite;
        int points;

        // Determine the type of apple based on the random number
        if (rand <= 10)
        {
            chosenSprite = goldenSprite;
            points = 5;
        }
        else if (rand <= 20)
        {
            chosenSprite = rottenSprite;
            points = -3;
        }
        else
        {
            chosenSprite = normalSprite;
            points = 1;
        }

        // Randomly position the apple within the specified range
        GameObject apple = Instantiate(applePrefab);
        apple.transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 6f, 0f);
        apple.GetComponent<Apple>().Setup(chosenSprite, points);  
    }
}
