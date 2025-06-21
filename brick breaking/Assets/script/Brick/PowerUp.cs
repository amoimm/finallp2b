using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum PowerUpType { EnlargePaddle, Multiball, ExtraLife }

public class PowerUp : MonoBehaviour
{
    public PowerUpType type; // public pour voir dans l’inspecteur (debug)
    public float fallSpeed = 3f;

    void Start()
    {
        AssignRandomType(); // Choisir aléatoirement un pouvoir
    }

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Paddle"))
        {
            Destroy(gameObject);
            ApplyPowerUp();
        }
        else if (collision.gameObject.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }   
    }

    

    private void AssignRandomType()
    {
        int random = Random.Range(0, 3);
        type = (PowerUpType)random;
    }

    private void ApplyPowerUp()
    {
        switch (type)
        {
            case PowerUpType.EnlargePaddle:
                GameManager.Instance.EnlargePaddle();
                break;
            case PowerUpType.Multiball:
                GameManager.Instance.SpawnMultiball();
                break;
            case PowerUpType.ExtraLife:
                GameManager.Instance.AddLife();
                break;
        }
    }
}