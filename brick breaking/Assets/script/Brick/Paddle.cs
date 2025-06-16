using UnityEngine;

public class Paddle : MonoBehaviour
{
    [Header("Mouvement")]
    private float speed = 10f;
    private float minX = -12.5f;
    private float maxX = 12.5f;

    [Header("Power-Up")]
    private float enlargedScale = 2f;
    private float powerupDuration = 10f;

    private Vector3 originalScale; 
    public bool isEnlarged;
    private float powerupTimer = 10f;

    void Start()
    {
        originalScale = transform.localScale;
        isEnlarged = false;
    }

    void Update()
    { 
        Move();

        if (isEnlarged)
        {
            powerupTimer -= Time.deltaTime;
            if (powerupTimer <= 0f)
            {
                ResetScale();
            }
        }
    }

    void Move()
    {
        float move = Input.GetAxisRaw("Horizontal");
        Vector3 pos = transform.position;
        pos.x += move * speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }

    public void Enlarge()
    {
        Debug.Log($"Enlarge() called. isEnlarged = {isEnlarged}");

        if (!isEnlarged)
        {
            transform.localScale = new Vector3(originalScale.x * enlargedScale, originalScale.y, originalScale.z);
            isEnlarged = true;
            powerupTimer = powerupDuration;
        }
        else
        {
            powerupTimer = powerupDuration; // Remet à zéro si déjà agrandi
            Debug.Log("Paddle already enlarged — timer reset.");
        }
    }
    

    private void ResetScale()
    {
        transform.localScale = originalScale;
        isEnlarged = false;
        Debug.Log("Paddle reset to original size.");
    }
}