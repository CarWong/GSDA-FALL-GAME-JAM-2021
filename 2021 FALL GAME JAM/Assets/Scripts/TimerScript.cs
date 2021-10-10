using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public float timer = 240;
    public Text timeText;
    public bool timerRunning = false;

    private void Start()
    {
        // Starts the timer automatically
        timerRunning = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 0;
                Debug.Log("Time has run out!");
                //End round
                SceneManager.LoadScene("LosingScene");
            }
        }
        TimeDisplay(timer);
    }


    void TimeDisplay(float DisplayTime)
    {
        if (DisplayTime < 0)
        {
            DisplayTime = 0;
        }
        else if (DisplayTime > 0)
        {
            DisplayTime += 1;
        }

        float minutes = Mathf.FloorToInt(DisplayTime / 60);
        float seconds = Mathf.FloorToInt(DisplayTime % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}