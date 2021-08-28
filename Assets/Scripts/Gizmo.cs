using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    [Tooltip("Show the when selected or not")]
    public bool showConstant;

    public Color colour = Color.green;

    public float size;

    private void OnDrawGizmos()
    {
        if (showConstant) DrawGizmos();
    }

    private void OnDrawGizmosSelected()
    {
        if (!showConstant) DrawGizmos();
    }

    void DrawGizmos()
    {
        Gizmos.color = colour;

        Gizmos.DrawWireSphere(transform.position, size);
    }
}
