using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    private float speed = 5f;
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
    }
}
