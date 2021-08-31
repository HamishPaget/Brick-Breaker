using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public Transform ballLaunch;

    [SyncVar]
    public GameObject ball;

    public GameObject ballPrefab;

    public ControlsSO controls;

    public bool ballLaunched = false;

    public float launchAngle = 90f;

    public float launchSpeed = 2f;

    public PlayerMovement player;

    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(this);
            Debug.LogWarning("Duplicate Game Managers in Scene", gameObject);
        }
    }
    #endregion

    /*
    private void Start()
    {
        SpawnInitialBall();
    }

    void SpawnInitialBall()
    {
        //If we have an object pool for balls - get ball from object pool
        //Not using for this case as an object pool would take more than creating one ball
        //I would use an object pool if we were expecting lots of balls
        ball = (Instantiate(ballPrefab, ballLaunch.position, Quaternion.identity, ballLaunch));
        ballLaunched = false;
    }*/

    /*
    private void Update()
    {
        if (Input.GetButtonDown(controls.shootInputName))
        {
            LaunchBall();
        }
    }

    void LaunchBall()
    {
        if (ballLaunched) return;

        
    }*/

    public void RemoveBall(GameObject ball)
    {
        StartCoroutine(RespawnBall());
    }

    IEnumerator RespawnBall()
    {
        
        yield return new WaitForSeconds(0.5f);
        //SpawnInitialBall();
        //Respawn Ball
        ball.GetComponent<Ball>().Respawn();
    }

    /*
    #region gizmos


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(ballLaunch.transform.position, ballLaunch.transform.position + Quaternion.Euler(0, 0, launchAngle / 2) * Vector3.up);
        Gizmos.DrawLine(ballLaunch.transform.position, ballLaunch.transform.position + Quaternion.Euler(0, 0, -launchAngle / 2) * Vector3.up);
    }

    #endregion
    */

}
