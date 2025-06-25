using UnityEngine;

public class EnemyShoot1:MonoBehaviour
{
    public GameObject impactEffectPrefab;

      private void OnCollisionEnter2D(Collision2D other)
      {
          if (other.gameObject.CompareTag("DeadZone"))
          {
              Destroy(gameObject);
          }
          else if (other.gameObject.CompareTag("Player"))
          {
              Instantiate(impactEffectPrefab, other.transform.position , Quaternion.identity);
              Destroy(gameObject);
              GameManagerSpace.Instance.LessLive();
          }
          else if (other.gameObject.CompareTag("Projectile"))
          {
              Destroy(gameObject);
              Instantiate(impactEffectPrefab, other.transform.position , Quaternion.identity);
              Destroy(other.gameObject);

          }
      }
}