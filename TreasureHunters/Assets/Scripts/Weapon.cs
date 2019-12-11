﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    GameObject owner;

    private void Awake()
    {
        owner = transform.parent.gameObject;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.Equals(owner))
        {
            Debug.Log("hit");
            collision.gameObject.GetComponent<PlayerController>().Animator.SetTrigger("TakeHit");
        }
    }
}