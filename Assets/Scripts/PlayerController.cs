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
    public TMP_Text scoreIiText;
    public TMP_Text scoreIiiText;
    public TMP_Text scoreIvText;
    public TMP_Text scoreVText;

    public int count;
    public int highScore;
    public int scoreII;
    public int scoreIII;
    public int scoreIV;
    public int scoreV;
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
            count = count + 1;
            SetScoreText();
        }
    }

    void SetScoreText()
    {
        scoreText.text = count.ToString() + "  <sprite=0>";
        //if (count >= highScore)
        //{
        //    highScore = count;
        //    highScoreText.text = "<sprite=0>" + count.ToString();
        //    SavePlayer();
        //}
        //else if (count < highScore)
        //{
        //    highScoreText.text = "<sprite=0>" + highScore.ToString();
        //    SavePlayer();
        //}
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (count >= highScore)
            {
                ScoreVscoreIV();
                ScoreIVscoreIII();
                ScoreIIIscoreII();
                scoreII = highScore;
                scoreIiText.text = highScore.ToString();
                highScore = count;
                highScoreText.text = count.ToString();
                SavePlayer();
            }
            else if (count < highScore & count > scoreII)
            {
                ScoreVscoreIV();
                ScoreIVscoreIII();
                ScoreIIIscoreII();
                scoreII = count;
                scoreIiText.text = count.ToString();
                HighScoreText();
                SavePlayer();
            }
            else if (count < scoreII & count > scoreIII)
            {
                ScoreVscoreIV();
                ScoreIVscoreIII();
                scoreIII = count;
                scoreIiiText.text = count.ToString();
                ScoreIIText();
                HighScoreText();
                SavePlayer();
            }
            else if (count < scoreIII & count > scoreIV)
            {
                ScoreVscoreIV();
                scoreIV = count;
                scoreIvText.text = count.ToString();
                ScoreIIIText();
                ScoreIIText();
                HighScoreText();
                SavePlayer();
            }
            else if (count < scoreIV & count > scoreV)
            {
                scoreV = count;
                scoreVText.text = count.ToString();
                ScoreIVText();
                ScoreIIIText();
                ScoreIIText();
                HighScoreText();
                SavePlayer();
            }
            else //if(count < scoreV)
            {
                scoreVText.text = scoreV.ToString();
                ScoreIVText();
                ScoreIIIText();
                ScoreIIText();
                HighScoreText();
                SavePlayer();
            }
        }
    }
    void ScoreVscoreIV()
    {
        scoreV = scoreIV;
        scoreVText.text = scoreIV.ToString();
    }
    void ScoreIVscoreIII()
    {
        scoreIV = scoreIII;
        scoreIvText.text = scoreIII.ToString();
    }
    void ScoreIIIscoreII()
    {
        scoreIII = scoreII;
        scoreIiiText.text = scoreII.ToString();
    }
    void HighScoreText()
    {
        highScoreText.text = highScore.ToString();
    }
    void ScoreIIText()
    {
        scoreIiText.text = scoreII.ToString();
    }
    void ScoreIIIText()
    {
        scoreIiiText.text = scoreIII.ToString();
    }
    void ScoreIVText()
    {
        scoreIvText.text = scoreIV.ToString();
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
        scoreII = data.scoreII;
        scoreIII = data.scoreIII;
        scoreIV = data.scoreIV;
        scoreV = data.scoreV;
        extraTime = data.extraTime;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
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
}
