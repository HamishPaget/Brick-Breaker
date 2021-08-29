using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DrawType {Box, Sphere, Trigger}
public class Gizmo : MonoBehaviour
{
    [Tooltip("Show the when selected or not")]
    public bool showConstant;

    public Color colour = Color.green;

    public float size;

    public DrawType shape = DrawType.Sphere;

    public bool wire = true;

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

        switch (shape)
        {
            case DrawType.Box:
                DrawBox();
                break;
            case DrawType.Sphere:
                DrawSphere();
                break;
            case DrawType.Trigger:
                DrawCollider();
                break;
            default:

                break;
        }
    }

    void DrawSphere()
    {
        if (wire)
        {
            Gizmos.DrawWireSphere(transform.position, size);
        }
        else
        {
            Gizmos.DrawSphere(transform.position, size);
        }
    }

    void DrawBox()
    {
        if (wire)
        {
            Gizmos.DrawWireCube(transform.position, Vector3.one * size);
        }
        else
        {
            Gizmos.DrawCube(transform.position, Vector3.one * size);
        }
    }

    void DrawCollider()
    {
        Collider triggerVolume = GetComponent<Collider>();

        //using bounds of collider for now
        Gizmos.DrawCube(triggerVolume.bounds.center, triggerVolume.bounds.size);
    }
}
