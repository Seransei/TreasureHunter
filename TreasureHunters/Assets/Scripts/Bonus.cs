using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
    [Range(0, 30)]public float respawnRate = 2f; // spawn rate in seconds
    bool disabled = false;
    float disabledTimer = 0f;
    public float bonusValue;

    private void Start()
    {
        respawnRate = 2f;
    }

    public abstract void GrantBonus(PlayerController player);

    private void Update()
    {
        if (disabled && disabledTimer <= 0f)
        {
            Enable();
        }
        else
        {
            disabledTimer -= Time.deltaTime;
        }
    }

    public void Enable()
    {
        disabled = false;
        disabledTimer = 0f;
        transform.Find("Capsule").gameObject.SetActive(true);
    }

    public void Disable()
    {
        disabled = true;
        disabledTimer = respawnRate;
        transform.Find("Capsule").gameObject.SetActive(false);
    }
}
