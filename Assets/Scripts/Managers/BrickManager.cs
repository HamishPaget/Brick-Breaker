using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    #region Singleton
    public static BrickManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.LogWarning("Found Multiple Brick Managers in scene", gameObject);
            Destroy(this);
            return;
        }
    }
    #endregion

    public List<IBrick> bricks = new List<IBrick>();

    public int TotalBricks()
    {
        int activeBricks = 0;

        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].active)
            {
                activeBricks++;
            }
        }

        return activeBricks;
    }

    public void ResetBricks()
    {
        for (int i = 0; i < bricks.Count; i++)
        {
            bricks[i].Enable();
        }
    }
}
