using System;
using UnityEngine;

public class PlayerShooting: MonoBehaviour
{
    public GameObject impactEffectPrefab;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            Destroy(gameObject);       // Détruit le projectile

        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);       // Détruit le projectile
            Instantiate(impactEffectPrefab, transform.position + new Vector3(1.2f, 0f, 0f), Quaternion.identity);
            Destroy(other.gameObject);       // Détruit l ennemi
        }

    }
}