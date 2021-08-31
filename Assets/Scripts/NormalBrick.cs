using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NormalBrick : NetworkBehaviour, IBrick
{
    public int score;

    public void Destroy()
    {
        ScoreManager.instance.AddScore(score);
        BrickManager.instance.bricks.Remove(this);
        Destroy(gameObject);
        //NetworkServer.UnSpawn(gameObject);
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
