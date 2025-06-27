using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPulseTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("isHover", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("isHover", false);
    }
}