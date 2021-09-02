using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NormalBrick : NetworkBehaviour, IBrick
{
    public int score;

    public bool active { get => gameObject.activeInHierarchy;}

    public void Destroy()
    {
        ScoreManager.instance.AddScore(score);
        gameObject.SetActive(false);
        //NetworkServer.UnSpawn(gameObject);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
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
