using UnityEngine;

public class AttackBonus : Bonus
{
    private void Start()
    {
        bonusValue = 0.1f;
    }

    public override void GrantBonus(PlayerController player)
    {
        player.attackMultiplier += bonusValue;
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
