using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool useMovementCurve;

    [Range(1,20)]
    public float moveSpeed = 4;

    public AnimationCurve moveSpeedOverTime;

    [Range(0, 1)]
    public float screenZone;

    public ControlsSO controls;


    float moveTimer;
    // Update is called once per frame
    void Update()
    {
        //Controller Dead Zone
        float moveDir = Input.GetAxis(controls.horizontalInputName);

        if (moveDir > controls.deadZone || moveDir < -controls.deadZone)
        {
            Move(moveDir);
            moveTimer += Time.deltaTime;
        }
        else
        {
            moveTimer = 0;
        }


    }

    void Move(float dir)
    {
        Debug.Log("Move");
        Vector3 pos = transform.position;

        if (useMovementCurve)
        {
            dir *= moveSpeedOverTime.Evaluate(moveTimer);
        }
        else
        {
            dir *= moveSpeed;
        }

        pos.x += dir * Time.deltaTime;

        pos = ClampToScreen(pos);

        transform.position = pos;
    }

    Vector3 ClampToScreen(Vector3 worldPos)
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(worldPos);
        pos.x = Mathf.Clamp(pos.x, screenZone / 2, 1 - (screenZone / 2));
        return Camera.main.ViewportToWorldPoint(pos);
    }
}
