﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8;
    public TMP_Text scoreText;
    public TMP_Text levelText;
    public float timeRemaining = 30;
    public bool timerIsRunning = false;
    public TMP_Text timeText;

    private Vector3 moveDir;
    private Rigidbody rb;
    private int count;

    Scene scene;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        count = 0;
        SetScoreText();
        SetLevelText();

        // Starts the timer automatically
        timerIsRunning = true;
    }
    void Update()
    {
        //moves player left and right
        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, 0).normalized;

        //player movement in Android device
        //PlayerLeftRight();

        TimerRunning();

        LoadNextLevel();
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);

        // moves the player in it's front direction
        rb.MovePosition(rb.position + this.transform.forward * moveSpeed * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetScoreText();
        }
    }
    void SetScoreText()
    {
        scoreText.text = count.ToString() + " <sprite=0>";
    }
    void SetLevelText()
    {
        scene = SceneManager.GetActiveScene();
        levelText.text = "Level " + scene.buildIndex;
    }
    void LoadNextLevel()
    {
        if (count == 15)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:0}", seconds);

        if (timeToDisplay <= 6)
        {
            timeText.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            timeText.color = new Color32(255, 255, 255, 255);
        } 
    }
    void TimerRunning()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                SceneManager.LoadScene("GameOver");
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
    void PlayerLeftRight()
    {
        //player movement in Android device
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    if (touch.position.x > (Screen.width / 2))
        //    {
        //        GoRight();
        //    }
        //    if (touch.position.x < (Screen.width / 2))
        //    {
        //        GoLeft();
        //    }
        //}
    }
}
