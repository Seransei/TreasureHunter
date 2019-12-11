using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Vector3 direction;
    [Range(0, 100)] public float speed = 10f;
    [Range(0, 200)] public float heightJump = 15f;

    public int numPlayer;

    [Header("Transition bool")]
    public bool running = false;
    public bool flip = false;
    public bool lookingLeft = true;
    public bool inCombat = false;
    public bool airborne = false;

    Animator animator;

    public Animator Animator
    {
        get { return animator; }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckInputs();

        if (flip)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            flip = false;
        }

        animator.SetBool("Running", running);
        animator.SetBool("Airborne", airborne);
        animator.SetBool("InCombat", inCombat);

        gameObject.transform.position = gameObject.transform.position + direction * Time.deltaTime;        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            airborne = false;
    }

    void CheckInputs()
    {
        direction.x = Input.GetAxis("Horizontal_J" + numPlayer) * speed;

        if (direction.x > 0)
        {
            running = true;
            if (lookingLeft)
            {
                flip = true;
                lookingLeft = false;
            }
        }
        else if (direction.x < 0)
        {
            running = true;
            if (!lookingLeft)
            {
                flip = true;
                lookingLeft = true;
            }
        }
        else
        {
            running = false;
        }

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
}
