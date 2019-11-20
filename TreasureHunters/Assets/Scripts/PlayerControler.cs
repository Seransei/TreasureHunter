﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    [Range(0, 100)] public float speed = 10f;
    [Range(0, 200)]public float heightJump = 15f;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal") * speed;
        //direction.y = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * heightJump, ForceMode2D.Impulse);
        }
        gameObject.transform.position = gameObject.transform.position + direction * Time.deltaTime;
       
    }
}
