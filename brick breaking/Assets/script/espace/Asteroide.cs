using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroide : MonoBehaviour
{
    [Header("Asteroid Settings")]
    [SerializeField] private List<GameObject> asteroidPrefabs; // 
    private float spawnInterval = 1.5f;
    private float minY = -4f;
    private float maxY = 4f;
    private float spawnX = 12f;
    private float despawnX = -12f;
    private float moveSpeed = 3f;
    private float rotationSpeed = 10f;
    private List<GameObject> activeAsteroids = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnAsteroids());
    }

    void Update()
    {
        MoveAndCleanupAsteroids();
    }

    IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            SpawnAsteroid();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnAsteroid()
    {
        if (asteroidPrefabs.Count == 0) return;

        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(spawnX, randomY, 0f);

        int randomIndex = Random.Range(0, asteroidPrefabs.Count);
        GameObject prefabToSpawn = asteroidPrefabs[randomIndex];

        GameObject newAsteroid = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        activeAsteroids.Add(newAsteroid);
    }

    void MoveAndCleanupAsteroids()
    {
        for (int i = activeAsteroids.Count - 1; i >= 0; i--)
        {
            GameObject asteroid = activeAsteroids[i];

            if (asteroid != null)
            {
                asteroid.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
                asteroid.transform.position += Vector3.left * moveSpeed * Time.deltaTime;

                if (asteroid.transform.position.x < despawnX)
                {
                    Destroy(asteroid);
                    activeAsteroids.RemoveAt(i);
                }
            }
        }
    }

}
