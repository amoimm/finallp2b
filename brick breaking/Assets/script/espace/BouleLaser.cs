using UnityEngine;
using System.Collections;

public class BouleLaser : MonoBehaviour
{
    public GameObject laser;

    void Start()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            float animTime = animator.GetCurrentAnimatorStateInfo(0).length;
            StartCoroutine(WaitAndSpawnLaser(animTime-0.01f));
        }
        else
        {
            Destroy(gameObject, 0.5f);
        }
    }

    private IEnumerator WaitAndSpawnLaser(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(laser, transform.position + new Vector3(-7.6f, 0f, 0f), Quaternion.identity);
        Destroy(gameObject);

    }
}