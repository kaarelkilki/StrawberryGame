using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class GameOver : MonoBehaviour
{
    public float timeRemaining = 3;
    public bool timerIsRunning = false;

    public TMP_Text scoreText;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        scoreText.text = GameObject.Find("Ursus").GetComponent<PlayerController>().count.ToString();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("aplause");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("Menu");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }

        
    }
}
