using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    Vector3 direction;
    [Range(0, 100)] public float speed = 10f;
    [Range(0, 200)] public float heightJump = 15f;

    public int numPlayer;

    [Header("Transition bool")]
    public bool lookingLeft = true;
    public bool inCombat = true;
    public bool airborne = false;

    float life = 100.0f;
    float moveInput;

    Animator animator;
    Rigidbody2D rb;
    GameObject lifeBar;
    Transform attackPoint;
    public LayerMask playerLayers;
    public float attackRange;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        lifeBar = GameObject.Find("Player" + numPlayer + "_Life");
        lifeBar.transform.GetChild(1).GetComponent<RawImage>().color = Color.Lerp(Color.red, Color.green, life / 100.0f);
        attackPoint = transform.GetChild(0).transform;
    }

    void Update()
    {
        CheckInputs();

        animator.SetBool("Airborne", airborne);
        animator.SetInteger("Speed", Mathf.FloorToInt(moveInput * speed));    
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal_J" + numPlayer);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(lookingLeft == false && moveInput < 0)
        {
            Flip();
        } 
        else if(lookingLeft == true && moveInput > 0)
        {
            Flip();
        } 
    }

    void Flip()
    {
        lookingLeft = !lookingLeft;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            airborne = false;
    }

    void CheckInputs()
    {
        if (Input.GetButtonDown("Jump_J" + numPlayer))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * heightJump, ForceMode2D.Impulse);
            airborne = true;
        }

        if (Input.GetButtonDown("Attack_J" + numPlayer))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        List<Collider2D> hits = new List<Collider2D>(Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers));
        PlayerController[] hitEnemies = hits.Select(hit => hit.GetComponent<PlayerController>()).Where(enemy => enemy != this).ToArray();

        foreach(PlayerController pc in hitEnemies)
        {
            pc.TakeDamage(20.0f);
        }
    }

    public void TakeDamage(float dmg)
    {
        life -= dmg;
        lifeBar.transform.GetChild(1).GetComponent<RawImage>().color = Color.Lerp(Color.red, Color.green, life / 100.0f);

        Vector3 scaler = lifeBar.transform.GetChild(1).localScale;
        scaler.x = life / 100.0f;
        lifeBar.transform.GetChild(1).localScale = scaler;

        if(life <= 0)
        {
            Die();
        }
        animator.SetTrigger("TakeHit");
    }

    void Die()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
    }

    void OnDrawGizmosSelected() 
    {
        if(attackPoint == null)
        return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
