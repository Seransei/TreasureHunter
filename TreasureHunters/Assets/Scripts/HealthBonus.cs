using UnityEngine;

public class HealthBonus : Bonus
{
    private void Start()
    {
        bonusValue = 30f;
    }

    public override void GrantBonus(PlayerController player)
    {
        player.Heal(bonusValue);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("kek");
        if (other.gameObject.layer == 8)
        {
            GrantBonus(other.gameObject.GetComponent<PlayerController>());
            Disable();
        }
    }
}