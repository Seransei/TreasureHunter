using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBonus : Bonus
{
    float value = 30f;

    public HealthBonus(float respawnRate) : base(respawnRate) {}

    public override void GrantBonus(PlayerController player)
    {
        Debug.Log(player.life);
        player.Heal(value);
        Debug.Log(player.life);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            Debug.Log(other.gameObject.GetComponent<PlayerController>().life);
            GrantBonus(other.gameObject.GetComponent<PlayerController>());
            Debug.Log(other.gameObject.GetComponent<PlayerController>().life);
            Disable();
        }
    }
}
