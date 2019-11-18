using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    public float speed = 10f;
    public float heightJump = 15f;
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

        if (Input.GetButton("Jump"))
            direction += Vector3.up * heightJump;

        gameObject.transform.position = gameObject.transform.position + direction * Time.deltaTime;
       
    }
}
