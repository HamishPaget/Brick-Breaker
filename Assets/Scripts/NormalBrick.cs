using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NormalBrick : MonoBehaviour, IBrick
{
    public UnityEvent onDestroy;

    public void Destroy()
    {
        onDestroy.Invoke();
        Destroy(gameObject);
    }

    public void Hit()
    {
        Destroy();
    }
}
