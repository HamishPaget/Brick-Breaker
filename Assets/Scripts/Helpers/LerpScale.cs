using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpScale : MonoBehaviour
{
    public bool setDefaultOnAwake = true;

    public Vector3 targetScale = Vector3.one;

    //[Range(0,1)]
    public float lerpSpeed;

    private void Awake()
    {
        if (setDefaultOnAwake)
        {
            targetScale = transform.localScale;
        }
    }

    private void Update()
    {
        if (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, lerpSpeed * Time.deltaTime);
        }
    }
}
