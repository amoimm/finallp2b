using UnityEngine;

public class Brick : MonoBehaviour
{
    public int points = 100;
    public bool isBonus = false;

    void Start()
    {
        GameManager.Instance.RegisterBrick();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance.AddScore(points);

            if (isBonus)
            {
                GameManager.Instance.SpawnBonus(transform.position);
            }

            GameManager.Instance.DeadBrick(gameObject);
        }
    }

    
}