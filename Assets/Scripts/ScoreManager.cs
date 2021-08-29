using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;

    public int score;

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
        scoreText.text = score.ToString();
        scoreText.transform.localScale = Vector3.one * 1.2f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddScore(5);
        }
    }
}
