using UnityEngine;

public class laser : MonoBehaviour
{
    public float targetWidth = 5f; // largeur finale (axe Y)
    public float growSpeed = 0.5f;   // vitesse de croissance

    private float animationLength;
    
    void Start()
    {
        // Détruit l'objet après 5 secondes
        Destroy(gameObject, 5f);
    }
   
    void Update()
    {
        // Augmente progressivement la largeur (axe Y) jusqu'à targetWidth
        if (transform.localScale.y < targetWidth)
        {
            float newY = Mathf.Min(transform.localScale.y + growSpeed * Time.deltaTime, targetWidth);
            transform.localScale = new Vector3(transform.localScale.x, newY, transform.localScale.z);
        }
    }
    
}