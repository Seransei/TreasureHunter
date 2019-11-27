using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    [Range(0, 100)] public float speed = 10f;
    [Range(0, 200)] public float heightJump = 15f;

    [Header("Transition bool")]
    public bool running = false;
    public bool flip = false;
    public bool lookLeft = true;
    public bool inCombat = false;
    public bool airborne = false;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal") * speed;
        //direction.y = Input.GetAxis("Vertical");

        if(direction.x > 0)
        {
            running = true;
            if (lookLeft)
            {
                flip = true;
                lookLeft = false;
            }
        }
        else if(direction.x < 0)
        {
            running = true;
            if (!lookLeft)
            {
                flip = true;
                lookLeft = true;
            }
        }
        else
        {
            running = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * heightJump, ForceMode2D.Impulse);
            airborne = true;
        }

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
}
