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
    public TMP_Text highScoreText;
    public int count;
    public int highScore;
    public float timeRemaining = 30;
    public float extraTime;
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
        TimerStarter();
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
    }

    void SetScoreText()
    {
        scoreText.text = count.ToString() + "  <sprite=0>";
        if (count >= highScore)
        {
            highScore = count;
            highScoreText.text = "<sprite=0>" + count.ToString();
            SavePlayer();
        }
        else if (count < highScore)
        {
            highScoreText.text = "<sprite=0>" + highScore.ToString();
            SavePlayer();
        }
    }

    void SetLevelText()
    {
        scene = SceneManager.GetActiveScene();
        levelText.text = "Level " + scene.buildIndex;
    }

    void AddLevelTime()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            timeRemaining = 30.0f;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            count = 0;
            timeRemaining = 29.0f;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            timeRemaining = timeRemaining + 23.2f;
            
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            timeRemaining = timeRemaining + 18.56f;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            timeRemaining = timeRemaining + 14.848f;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            timeRemaining = timeRemaining + 11.8784f;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            timeRemaining = 3.0f;
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
                SavePlayer();
                GameOver();
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void TimerStarter()
    {
        // Starts the timer automatically
        if (SceneManager.GetActiveScene().buildIndex == (1 | 2 | 3 | 4 | 5 | 6))
        {
            timerIsRunning = true;
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
        highScore = data.highScore;
        extraTime = data.extraTime;
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("quit");
    }

    public void PlayButton()
    {
        SavePlayer();
        SceneManager.LoadScene("Level1");
    }
}
