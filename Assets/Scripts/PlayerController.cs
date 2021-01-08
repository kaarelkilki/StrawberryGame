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
    public TMP_Text scoreTextGameOver;
    public TMP_Text levelText;
    public TMP_Text highScoreText;
    public TMP_Text timeText;
    public AudioSource collectStrawberry;

    public int count;
    public int highScore;
    public float timeRemaining = 30;
    public float extraTime;
    public bool timerIsRunning = false;
    
    private bool goLeft = false;
    private bool goRight = false;
    private Vector3 moveDir;
    public Rigidbody rb;

    [SerializeField]
    private Canvas menuCanvas;
    [SerializeField]
    private Canvas playCanvas;
    [SerializeField]
    private Canvas gameOverCanvas;
    [SerializeField]
    private Button extraTimeButton;
    [SerializeField]
    private Button adsButton;
    
    Scene scene;

    void Start()
    {
        LoadPlayer();
        SetMoveSpeed();
        SetCanvas();
        AddLevelTime();
        SetScoreText();
        SetLevelText();
        TimerStarter();
        collectStrawberry.Stop();
    }

    void Update()
    {
        //moves player left and right on computer
        #if UNITY_EDITOR
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, 0).normalized;
        #endif

        //player movement on Android device
        #if UNITY_ANDROID
            PlayerLeftRight();
        
            goRight = false;
            goLeft = false;
        #endif

        TimerRunning();
        
        LoadNextLevel();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            MenuCanvas();
        }
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
            collectStrawberry.Play();
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

    void SetCanvas()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            MenuCanvas();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
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
        else if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            menuCanvas.enabled = false;
            NotPlayCanvas();
        }
    }

    void SetScoreText()
    {
        scoreText.text = count.ToString() + "  <sprite=0>";
        scoreTextGameOver.text = count.ToString() + "  <sprite=0>";
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (count >= highScore)
            {
                highScore = count;
                highScoreText.text = "<sprite=0>" + count.ToString();
                SavePlayer();
            }
            else if (count < highScore)
            {
                highScoreText.text = "<sprite=0>" + highScore.ToString();
            }
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
            timeRemaining = 11.676f;
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
                GameOverCanvas();
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void PlayCanvas()
    {
        menuCanvas.enabled = false;
        playCanvas.enabled = true;
        gameOverCanvas.enabled = false;
    }

    void NotPlayCanvas()
    {
        playCanvas.enabled = false;
        gameOverCanvas.enabled = false;
    }

    void GameOverCanvas()
    {
        moveSpeed = 0;
        gameOverCanvas.enabled = true;
        menuCanvas.enabled = false;
        playCanvas.enabled = false;
        if (extraTime > 0.0f)
        {
            extraTimeButton.gameObject.SetActive(true);
        }
        else if (extraTime <= 0.0f)
        {
            extraTimeButton.gameObject.SetActive(false);
        }
    }

    void MenuCanvas()
    {
        moveSpeed = 0;
        menuCanvas.enabled = true;
        gameOverCanvas.enabled = false;
        playCanvas.enabled = false;
        if (extraTime > 0.0f)
        {
            adsButton.gameObject.SetActive(false);
        }
        else if (extraTime <= 0.0f)
        {
            adsButton.gameObject.SetActive(true);
        }
    }

    void TimerStarter()
    {
        // Starts the timer automatically
        if (SceneManager.GetActiveScene().buildIndex == (1 | 2 | 3 | 4 | 5 | 6))
        {
            timerIsRunning = true;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            timerIsRunning = false;
        }
    }

    public void MoveLeft()
    {
        goLeft = true;
    }

    public void MoveRight()
    {
        goRight = true;
    }
    void PlayerLeftRight()
    {
        //player movement in Android device
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > (Screen.width / 2))
            {
                MoveRight();
                if (goRight == true)
                {
                    moveDir = new Vector3(1, 0, 0).normalized;
                }
            }
            if (touch.position.x < (Screen.width / 2))
            {
                MoveLeft();
                if (goLeft ==  true)
                {
                    moveDir = new Vector3(-1, 0, 0).normalized;
                }
            }
        }
    }

    void SavePlayer()
    {
        ES3.Save("count", count);
        ES3.Save("timeRemaining", timeRemaining);
        ES3.Save("highScore", highScore);
        ES3.Save("extraTime", extraTime);
    }

    void LoadPlayer()
    {
        if (ES3.KeyExists("count"))
            count = ES3.Load<int>("count");
        if (ES3.KeyExists("timeRemaining"))
            timeRemaining = ES3.Load<float>("timeRemaining");
        if (ES3.KeyExists("highScore"))
            highScore = ES3.Load<int>("highScore");
        if (ES3.KeyExists("extraTime"))
            extraTime = ES3.Load<float>("extraTime");
    }

    public void ExitButton()
    {
        count = 0;
        SavePlayer();
        Application.Quit();
    }

    public void PlayButton()
    {
        SavePlayer();
        SceneManager.LoadScene("Level1");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void AddExtraTime()
    {
        timeRemaining = timeRemaining + 6.0f;
        timerIsRunning = true;
        extraTime = extraTime - 6.0f;
        SetMoveSpeed();
        PlayCanvas();
    }
}
