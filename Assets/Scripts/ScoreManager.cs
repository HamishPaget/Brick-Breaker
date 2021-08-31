using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ScoreManager : NetworkBehaviour
{
    public Text scoreText;

    [SyncVar]
    public int score;

    int prevScore;

    #region Singleton
    public static ScoreManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Debug.LogWarning("Found Multiple Score Managers in scene", gameObject);
            Destroy(this);
            return;
        }
    }
    #endregion

    public void AddScore(int amount)
    {
        score += amount;
        
    }

    private void Update()
    {
        if (score != prevScore)
        {
            scoreText.text = score.ToString();
            scoreText.transform.localScale = Vector3.one * 1.2f;
        }

        prevScore = score;
    }
}
