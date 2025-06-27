using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject ennemyPrefab;
    [SerializeField] private Sprite ennemy1;
    [SerializeField] private Sprite ennemy2;
    [SerializeField] private Sprite ennemy3;
    private float spawnTimer = 1.5f;

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
        spawnTimer -= Time.deltaTime;

        float minInterval = 0.5f;
        float maxInterval = Mathf.Max(3.5f-Time.time*0.01f, minInterval);
        
        if (spawnTimer <= 0)
        {
            SpawnEnnemy();
            spawnTimer = Random.Range(maxInterval*0.8f, maxInterval*1.2f);
        }
    }
    
    private void SpawnEnnemy()
    {
        int rand = Random.Range(1, 101);
        Sprite chosenSprite;
        int points;
        int lives;

        // Determine the type of ennemy based on the random number
        if (rand <= 40)
        {
            chosenSprite = ennemy1;
            points = 100;
            lives = 1;
        }
        else if (rand <= 80)
        {
            chosenSprite = ennemy2;
            points = 300;
            lives = 3;
        }
        else
        {
            chosenSprite = ennemy3;
            points = 500;
            lives = 5;
        }

        // Randomly position the ennemy within the specified range
        GameObject ennemy = Instantiate(ennemyPrefab);
        ennemy.transform.position = new Vector3(10f, Random.Range(-4.5f, 3.2f), 0f);
        ennemy.GetComponent<Ennemy>().Setup(chosenSprite, points, lives);
    }
}
