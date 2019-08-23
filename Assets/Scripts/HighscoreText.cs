using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreText : MonoBehaviour
{

    public static HighscoreText Instance;

	public Text highScore;

    public void Start()
    {
        Instance = this;
    }

	void OnEnable()
    {
        highScore = GetComponent<Text>();
		highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
	}

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        highScore.text = "0";
    }
}
