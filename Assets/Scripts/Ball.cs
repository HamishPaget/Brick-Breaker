using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody))]
public class Ball : NetworkBehaviour
{

    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public PlayerMovement mainPlayer;

    public float ballSpeed = 2f;

    [SyncVar]
    public bool inLauncher;

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

        if (inLauncher & NetworkClient.isHostClient)
        {
            transform.position = mainPlayer.ballLaunchLocation.position;
        }
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

    public void Respawn()
    {
        SpawnDeathParticle();

        if (NetworkClient.isHostClient){

            inLauncher = true;
            mainPlayer.ballToLaunch = gameObject;
            rb.velocity = Vector3.zero;
        }
    }

    public GameObject deathParticle;
    private void SpawnDeathParticle()
    {
        if (deathParticle != null)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity, null);
        }
    }

    public void LaunchBall()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-GameManager.instance.launchAngle / 2, GameManager.instance.launchAngle / 2));


        rb.velocity = transform.up * ballSpeed;
        inLauncher = false;
    }
}
