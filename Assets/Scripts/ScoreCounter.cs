using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance;

    public Text scoreText;
    public int score;

    public void Start()
    {
        Instance = this;

        score = 0;
        InvokeRepeating("ScoreUpdate", 4.0f, 0.5f);
    }

    public void Update()
    {
        scoreText.text = "Score: " + score;
    }

    public void ScoreUpdate()
    {
        if(GameManager.Instance.isGameOver)
        {
            return;
        }

        score += 1 * GameManager.Instance.scoreMultiplier;
    }
}
