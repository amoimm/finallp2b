using UnityEngine;

public class BrickGridGenerator : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.GenerateGrid();
    }

    
}