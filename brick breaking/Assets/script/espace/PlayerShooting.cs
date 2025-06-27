using System;
using UnityEngine;

public class PlayerShooting: MonoBehaviour
{
    public GameObject impactEffectPrefab;
    private float speed = 2f;           // Vitesse initiale
    private float acceleration = 4f;    // Accélération par seconde
    private float currentSpeed;

    private void Start()
    {
        currentSpeed = speed;
        transform.rotation = Quaternion.Euler(0f, 0f, 180f); // Rotation visuelle
    }

    private void Update()
    {
        // Augmente la vitesse avec le temps
        currentSpeed += acceleration * Time.deltaTime;

        // Déplace vers la droite dans l'espace monde
        transform.position += Vector3.right * currentSpeed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            // Destroy the projectile when out of the playzone
            Destroy(gameObject);
        }
    }
}