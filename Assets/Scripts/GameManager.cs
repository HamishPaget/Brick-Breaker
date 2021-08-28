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

    private void Awake()
    {
        balls.Add(Instantiate(ballPrefab, ballLaunch));
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

        //Add force randomly upwards
        balls[0].GetComponent<Rigidbody>().velocity = balls[0].transform.up * launchSpeed;
        balls[0].transform.SetParent(null);
        ballLaunched = true;
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
