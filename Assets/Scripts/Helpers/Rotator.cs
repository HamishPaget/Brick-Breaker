using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotation;

    private void FixedUpdate()
    {
        transform.Rotate(rotation * Time.fixedDeltaTime);
    }
}
