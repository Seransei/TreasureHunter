using System.Collections;
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
    public bool inCombat = false;
    public bool airborne = false;

    float life = 100.0f;
    float moveInput;

    Animator animator;
    Rigidbody2D rb;
    GameObject lifeBar;

    public Animator Animator
    {
        get { return animator; }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        lifeBar = GameObject.Find("Player" + numPlayer + "_Life");
        lifeBar.transform.GetChild(1).GetComponent<RawImage>().color = Color.Lerp(Color.red, Color.green, life / 100.0f);
    }

    void Update()
    {
        CheckInputs();

        animator.SetBool("Airborne", airborne);
        animator.SetInteger("Speed", Mathf.FloorToInt(moveInput * speed));

        /*if (flip)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            flip = false;
        }*/

        //animator.SetBool("Running", running);
        //animator.SetBool("Airborne", airborne);
        //animator.SetBool("InCombat", inCombat);

        //gameObject.transform.position = gameObject.transform.position + direction * Time.deltaTime;        
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
            animator.SetTrigger("Attack");
        }
    }

    public void TakeDamage(float dmg)
    {
        life -= dmg;
        lifeBar.transform.GetChild(1).GetComponent<RawImage>().color = Color.Lerp(Color.red, Color.green, life / 100.0f);
        lifeBar.transform.GetChild(1).localScale = new Vector3(life / 100.0f, 1f, 1f);

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
}
