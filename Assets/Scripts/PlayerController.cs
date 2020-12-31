using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public TMP_Text scoreText;
    public TMP_Text levelText;
    public TMP_Text highScoreText;
    public TMP_Text timeText;

    public int count;
    public int highScore;
    public float timeRemaining = 30;
    public float extraTime;
    public bool timerIsRunning = false;
    
    private Vector3 moveDir;
    private Rigidbody rb;

    [SerializeField]
    private Canvas playCanvas;
    [SerializeField]
    private Canvas gameOverCanvas;
    [SerializeField]
    private Button extraTimeButton;

    Scene scene;

    void Start()
    {
        LoadPlayer();
        rb = GetComponent<Rigidbody>();

        SetMoveSpeed();
        SetPlayCanvas();
        SetPlayCanvas();
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
            count = count + 1;
            SetScoreText();
        }
    }

    void SetMoveSpeed()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            moveSpeed = 8;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            moveSpeed = 9;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            moveSpeed = 10;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            moveSpeed = 11;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            moveSpeed = 12;
        }
        else
        {
            moveSpeed = 0;
        }
    }

    void SetPlayCanvas()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            PlayCanvas();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            PlayCanvas();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            PlayCanvas();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            PlayCanvas();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            PlayCanvas();
        }
        else
        {
            playCanvas.enabled = false;
            gameOverCanvas.enabled = false;
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
                if (count == 30)
                {
                    SavePlayer();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                break;
            case 3:
                if (count == 45)
                {
                    SavePlayer();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                break;
            case 4:
                if (count == 60)
                {
                    SavePlayer();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                break;
            case 5:
                if (count == 75)
                {
                    SavePlayer();
                    WinScreen();
                }
                break;
        }
    }

    void WinScreen()
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
                //WinScreen();
                GameOverCanvas();
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void PlayCanvas()
    {
        playCanvas.enabled = true;
        gameOverCanvas.enabled = false;
        
    }

    void GameOverCanvas()
    {
        moveSpeed = 0;
        playCanvas.enabled = false;
        gameOverCanvas.enabled = true;
        if (extraTime > 0.0f)
        {
            extraTimeButton.gameObject.SetActive(true);
        }
        else if (extraTime <= 0.0f)
        {
            extraTimeButton.gameObject.SetActive(false);
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

        //Vector3 position;
        //position.x = data.position[0];
        //position.y = data.position[1];
        //position.z = data.position[2];
    }

    public void ExitButton()
    {
        SavePlayer();
        Application.Quit();
        Debug.Log("quit");
    }

    public void PlayButton()
    {
        SavePlayer();
        SceneManager.LoadScene("Level1");
    }

    public void Menu()
    {
        SavePlayer();
        SceneManager.LoadScene("Menu");
    }

    public void AddExtraTime()
    {
        timeRemaining += 6.0f;
        timerIsRunning = true;
        extraTime -= 6.0f;
        SetMoveSpeed();
        PlayCanvas();
    }
}
