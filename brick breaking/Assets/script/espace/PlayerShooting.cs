using System;
using UnityEngine;

public class PlayerShooting: MonoBehaviour
{
    public GameObject impactEffectPrefab;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);       // Détruit le projectile

        if (other.gameObject.CompareTag("DeadZone"))
        {

        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(impactEffectPrefab, transform.position + new Vector3(1.2f, 0f, 0f), Quaternion.identity);
            Destroy(other.gameObject);       // Détruit l ennemi
        }

    }
}