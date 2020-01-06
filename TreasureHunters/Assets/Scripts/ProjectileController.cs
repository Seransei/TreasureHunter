using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 5.0f;
    public PlayerController player;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        other.gameObject.GetComponent<PlayerController>().TakeDamage(player.attackDamage * player.attackMultiplier);
        Destroy(gameObject);
    }
}
