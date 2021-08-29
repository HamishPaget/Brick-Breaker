using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.gameObject.GetComponent<Ball>() != null)
        {
            GameManager.instance.RemoveBall(other.attachedRigidbody.gameObject);
        }
    }
}
