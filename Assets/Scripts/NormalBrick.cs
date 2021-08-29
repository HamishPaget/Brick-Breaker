using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBrick : MonoBehaviour, IBrick
{
    public int score;

    public void Destroy()
    {
        ScoreManager.instance.AddScore(score);
        BrickManager.instance.bricks.Remove(this);
        Destroy(gameObject);
    }

    public void Hit()
    {
        Destroy();
    }

    private void Start()
    {
        BrickManager.instance.bricks.Add(this);
    }
}
