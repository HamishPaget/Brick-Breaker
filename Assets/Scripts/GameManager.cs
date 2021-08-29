using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform ballLaunch;

    public List<GameObject> balls;

    public GameObject ballPrefab;

    public ControlsSO controls;

    public bool ballLaunched = false;

    public float launchAngle = 90f;

    public float launchSpeed = 2f;


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

    private void Start()
    {
        SpawnInitialBall();
    }

    void SpawnInitialBall()
    {
        //If we have an object pool for balls - get ball from object pool
        //Not using for this case as an object pool would take more than creating one ball
        //I would use an object pool if we were expecting lots of balls
        balls.Add(Instantiate(ballPrefab, ballLaunch.position, Quaternion.identity, ballLaunch));
        ballLaunched = false;
    }

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

        balls[0].transform.rotation = Quaternion.Euler(0, 0, Random.Range(-launchAngle / 2, launchAngle / 2));

        
        balls[0].GetComponent<Rigidbody>().velocity = balls[0].transform.up * launchSpeed;
        balls[0].transform.SetParent(null);
        ballLaunched = true;
    }

    public void RemoveBall(GameObject ball)
    {
        if (balls.Contains(ball) == false)
        {
            return;
        }

        balls.Remove(ball);

        //If we had a object pool for balls - remove ball from play and put back in play
        Destroy(ball);

        CheckForBalls();
    }

    public void CheckForBalls()
    {
        if (balls.Count > 0) { return; }

        //We only get here if there are no balls left
        Debug.Log("No More Balls");
        StartCoroutine(RespawnBall());
    }

    IEnumerator RespawnBall()
    {
        yield return new WaitForSeconds(0.5f);
        SpawnInitialBall();
    }

    #region gizmos


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(ballLaunch.transform.position, ballLaunch.transform.position + Quaternion.Euler(0, 0, launchAngle / 2) * Vector3.up);
        Gizmos.DrawLine(ballLaunch.transform.position, ballLaunch.transform.position + Quaternion.Euler(0, 0, -launchAngle / 2) * Vector3.up);
    }

    #endregion

}
