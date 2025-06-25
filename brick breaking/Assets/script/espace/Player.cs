using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] private GameObject player;
    private float speed = 5f;
    [SerializeField] private GameObject bulletPrefab;      // Le prefab du projectile à instancier
    [SerializeField] private Transform firePoint; // Le point de départ du tir (souvent un Empty enfant du joueur)
    [SerializeField] private Transform firePoint2;          // Le point de départ du tir (souvent un Empty enfant du joueur)

    private float bulletSpeed = 10f;      // Vitesse du projectile
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Entrée clavier
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        
        //Deplacement
        Vector3 direction = new Vector3(x, y, 0f).normalized;
        player.transform.position += direction * speed * Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("A");
            GameManagerSpace.Instance.FireEnemyBullets3(firePoint2);
            
        }
        
    }
    
}
