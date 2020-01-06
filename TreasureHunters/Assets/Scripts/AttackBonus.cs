using System;
using UnityEngine;

class AttackBonus : Bonus
{
    float value = 0.1f;

    public AttackBonus(float respawnRate) : base(respawnRate) { }

    public override void GrantBonus(PlayerController player)
    {
        player.attackMultiplier += value;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            GrantBonus(other.gameObject.GetComponent<PlayerController>());
            Disable();
        }
    }
}
