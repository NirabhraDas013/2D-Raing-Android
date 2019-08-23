using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button pauseButton;
    public Sprite pauseImage;
    public Sprite playImage;

    public void Start()
    {
        //countdownText.SetActive(false);
        pauseButton.GetComponent<Image>().sprite = pauseImage;
    }

    public void PauseButton()
    {
        if (Time.timeScale == 1)
        {
            pauseButton.GetComponent<Image>().sprite = playImage;
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0)
        {
            //Countdown(timeLeft);
            pauseButton.GetComponent<Image>().sprite = pauseImage;
            Time.timeScale = 1;
        }
    }
}