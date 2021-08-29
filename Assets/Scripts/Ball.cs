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

        Vector3 vel = rb.velocity;

        //If the ball is traveling horizontally & won't return to the player
        if (Mathf.Abs(vel.y) < 0.01f)
        {
            vel.y += Random.Range(-1f,1f);

            rb.velocity = vel.normalized * ballSpeed;
        }

        //If the ball is traveling Vertically & won't hit anymore blocks
        if (Mathf.Abs(vel.x) < 0.01f)
        {
            vel.x += Random.Range(-1f, 1f);

            rb.velocity = vel.normalized * ballSpeed;
        }
    }

    public GameObject deathParticle;
    private void OnDestroy()
    {
        if (deathParticle != null)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity, null);
        }
    }
}
