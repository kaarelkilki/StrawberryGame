using System.Collections;
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
    public int count;
    public float timeRemaining = 30;
    public bool timerIsRunning = false;
    public TMP_Text timeText;

    private Vector3 moveDir;
    private Rigidbody rb;
    
    Scene scene;

    void Start()
    {
        LoadPlayer();
        rb = GetComponent<Rigidbody>();

        AddLevelTime();
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
            count = count + SceneManager.GetActiveScene().buildIndex;
            SetScoreText();
        }
        else if(other.gameObject.CompareTag("Obstacles"))
        {
            GameOver();
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

    void AddLevelTime()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            timeRemaining = 35.0f;
            count = 0;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            timeRemaining = timeRemaining + 25.0f;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            timeRemaining = timeRemaining + 15.0f;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            timeRemaining = timeRemaining + 10.0f;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            timeRemaining = timeRemaining + 5.0f;
        }
    }

    void LoadNextLevel()
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                if (count == 15)
                {
                    SavePlayer();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                break;
            case 2:
                if (count == 45)
                {
                    SavePlayer();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                break;
            case 3:
                if (count == 90)
                {
                    SavePlayer();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                break;
            case 4:
                if (count == 150)
                {
                    SavePlayer();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                break;
            case 5:
                if (count == 225)
                {
                    SavePlayer();
                    GameOver();
                }
                break;
        }
    }

    void GameOver()
    {
        Debug.Log("GameOver");
        SceneManager.LoadScene("GameOver");
        LoadPlayer();
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
                GameOver();
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

    void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        count = data.count;
        timeRemaining = data.timeRemaining;
    }
}
