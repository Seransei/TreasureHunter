using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
    [Range(0, 10)]public float spawnRate = 3; // spawn rate in seconds

    public abstract void GrantBonus(PlayerController player);
}
