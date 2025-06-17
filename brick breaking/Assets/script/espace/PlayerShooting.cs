using System;
using UnityEngine;

public class ProjectilePlayer : Projectile
{
    public GameObject impactEffectPrefab;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            
            Instantiate(impactEffectPrefab, transform.position + new Vector3(1.2f, 0f, 0f), Quaternion.identity);
            Destroy(gameObject);       // Détruit le projectile
        }
    }

    
}