using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            // Récupère la durée de l’animation actuelle
            float animTime = animator.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, animTime);
        }
        else
        {
            // Sécurité : détruit quand même au bout de 0.5 sec
            Destroy(gameObject, 0.5f);
        }
    }
}