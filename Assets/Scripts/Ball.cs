using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public float ballSpeed = 2f;

    private void FixedUpdate()
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);

        Vector3 velocity = rb.velocity;

        if (screenPos.x > 1 || screenPos.x < 0)
        {
            Debug.Log("Left Side of screen");
            velocity.x = -velocity.x;
        }

        if (screenPos.y > 1)
        {
            Debug.Log("Left Top of screen");


            velocity.y = -velocity.y;
        }

        /*
        if (velocity != rb.velocity)
        {
        */
            rb.velocity = velocity.normalized * ballSpeed;
        //}

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<IBrick>() != null)
        {
            collision.gameObject.GetComponent<IBrick>().Hit();
        }
    }
}
