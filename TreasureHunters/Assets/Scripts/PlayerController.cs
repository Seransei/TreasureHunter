using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{

    Vector3 direction;
    [Range(0, 100)] public float speed = 10f;
    [Range(0, 200)] public float heightJump = 15f;

    public int numPlayer;

    public GameObject projectilePrefab;

    [Header("Transition bool")]
    public bool lookingLeft = false;
    public bool inCombat = true;
    public bool airborne = false;

    [Header("Stats")]
    public float life = 100.0f;
    public float attackDamage = 10f;
    public float attackMultiplier = 1f;
    public float attackRange;
    float attackRate = 2.0f;
    float nextAttackTime = 0f;
    float guardRate = 1f;

    float moveInput;

    Animator animator;
    Rigidbody2D rb;
    GameObject lifeBar, guardBar;
    Transform attackPoint;
    public LayerMask playerLayers;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        lifeBar = GameObject.Find("Player" + numPlayer + "_Life");
        lifeBar.transform.GetChild(1).GetComponent<RawImage>().color = Color.Lerp(Color.red, Color.green, life / 100.0f);
        
        guardBar = GameObject.Find("Player" + numPlayer + "_Guard");
        attackPoint = transform.GetChild(0).transform;
    }

    void Update()
    {
        if(life > 0)
        {
            CheckInputs();

            animator.SetBool("Airborne", airborne);
            animator.SetInteger("Speed", Mathf.FloorToInt(moveInput * speed)); 

            if(guardRate < 1f)
                guardRate += 0.01f * Time.deltaTime;  

            Vector3 scaler = guardBar.transform.GetChild(1).localScale;
            scaler.x = guardRate;
            guardBar.transform.GetChild(1).localScale = scaler;

        }
        UpdateLifebar();
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
            if(Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Input.GetButtonDown("HeavyAttack_J" + numPlayer))
        {
            if(Time.time >= nextAttackTime)
            {
                Attack();
                GameObject go = Instantiate(projectilePrefab, transform.GetChild(0).position, Quaternion.Euler(0f, 0f, 90f), transform);
                go.GetComponent<ProjectileController>().player = this;
                if(!lookingLeft)
                      go.transform.Rotate(Vector3.forward, 180f);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Input.GetButtonDown("Guard_J" + numPlayer))
        {
            Guard();
        }
        if (Input.GetButtonUp("Guard_J" + numPlayer)) 
        {
            animator.SetBool("Guard", false);
        }

    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        List<Collider2D> hits = new List<Collider2D>(Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers));
        PlayerController[] hitEnemies = hits.Select(hit => hit.GetComponent<PlayerController>()).Where(enemy => enemy != this).ToArray();

        foreach(PlayerController pc in hitEnemies)
        {
            pc.TakeDamage(attackDamage * attackMultiplier);
        }
    }

    void Guard()
    {
        if(!animator.GetBool("Guard"))
            animator.SetBool("Guard", true);
    }

    public void TakeDamage(float dmg)
    {
        if(animator.GetBool("Guard"))
        {
            life -= (dmg - dmg * guardRate); 
            guardRate -= 0.2f;
            if(guardRate < 0)
                guardRate = 0;
        }
        else
            life -= dmg;
        
        if(life <= 0)
        {
            life = 0;
            Die();
        }
        animator.SetTrigger("TakeHit");
    }

    public void UpdateLifebar()
    {
        lifeBar.transform.GetChild(1).GetComponent<RawImage>().color = Color.Lerp(Color.red, Color.green, life / 100.0f);
        Vector3 scaler = lifeBar.transform.GetChild(1).localScale;
        scaler.x = life / 100.0f;
        lifeBar.transform.GetChild(1).localScale = scaler;

    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        speed = 0;
        //GetComponent<BoxCollider2D>().enabled = false;
        //this.enabled = false;
    }
    
    public void Heal(float hp)
    {
        life += hp;
        if (life > 100f)
            life = 100f;
    }

    void OnDrawGizmosSelected() 
    {
        if(attackPoint == null)
        return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}