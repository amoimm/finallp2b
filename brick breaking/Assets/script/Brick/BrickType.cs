using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class BrickType
{
    public Color color;
    public int points;
    public float spawnChance; // En pourcentage
    public bool isBonus;
}