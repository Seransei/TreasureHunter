﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControler : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        other.gameObject.GetComponent<PlayerController>().TakeDamage(20.0f);
        Destroy(gameObject);
    }
}
