using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndlessRunner : MonoBehaviour
{
    public float moveSpeed;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text playLivesText;
    public TMP_Text menuLivesText;
    public TMP_Text endScoreText;
    public AudioSource collectStrawberry;

    public int count;
    public int highScore;
    public Rigidbody rb;

    private Vector3 moveDir;
    public int lives;
    public bool timerIsRunning = false;
    public float timeRemaining = 3f;
    private float inputTimer;
    bool collision;
    
    [SerializeField]
    private Canvas menuCanvas;
    [SerializeField]
    private Canvas playCanvas;
    [SerializeField]
    private Canvas gameOverCanvas;
    [SerializeField]
    private Button adsButton;

    Scene scene;

    void Start()
    {
        collectStrawberry.Stop();
        LoadPlayer();
        MenuCanvas();
        SetText();
        inputTimer = 0;
    }

    void Update()
    {
        PlayerLeftRight();
        ActivateAdsButton();
        InputTimer();
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
            moveSpeed = 8 + (count / 5);
            SetText();
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Obstacles") && collision == false)
        {
            collision = true;
            if (lives < 0)
            {
                SavePlayer();
                GameOver();
            }
            else if (lives >= 0)
            {
                lives = lives - 1;
                SetText();
            }
        }
    }

    private void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.CompareTag("Obstacles") && collision == true)
        {
            collision = false;
        }
    }

    void SetText()
    {
        playLivesText.text = (lives + 1).ToString() + "   <sprite=2>";
        menuLivesText.text = (lives + 1).ToString() + "   <sprite=2>";
        if (count >= highScore)
        {
            highScore = count;
            highScoreText.text = "<sprite=0>" + count.ToString();
            scoreText.text = "<sprite=0>" + count.ToString() + "/" + count.ToString();
        }
        else if (count < highScore)
        {
            highScoreText.text = "<sprite=0>" + highScore.ToString();
            scoreText.text = "<sprite=0>" + count.ToString() + "/" + highScore.ToString();
        }
    }

    void PlayerLeftRight()
    {
        //moves player left and right on computer
#if UNITY_EDITOR
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, 0).normalized;
#endif

        //player movement on Android device
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch lastTouch = Input.GetTouch(0);
            if (Input.touches.Length > 0)
            {
                lastTouch = Input.touches[Input.touches.Length - 1];
            }
            
            if (lastTouch.position.x > (Screen.width / 2))
            {
                moveDir = new Vector3(1, 0, 0).normalized;
            }
            else if (lastTouch.position.x < (Screen.width / 2))
            {
                moveDir = new Vector3(-1, 0, 0).normalized;
            }
            else if (lastTouch.position.x > (Screen.width / 2) & lastTouch.position.x < (Screen.width / 2))
            {
                moveDir = new Vector3(0, 0, 0).normalized;
            }

            if (lastTouch.phase == TouchPhase.Ended)
            {
                moveDir = new Vector3(0, 0, 0).normalized;
            }
        }
#endif
    }

    void InputTimer()
    {
        inputTimer += Time.deltaTime;
        //Then you do your usual input checks
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Reset the timer
            inputTimer = 0;
            //Do player action
        }
        //5 seconds or anything you want, I would turn that into a public field so you can change it from the inspector
        if (inputTimer >= 5f)
        {
            inputTimer = 0;
            if (playCanvas.enabled == true)
            {
                GameOver();
                Debug.Log("GameOver");
            }
        }
    }

    void AplauseTimer()
    {
        timerIsRunning = true;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("aplause");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("EndlessRunner");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void MenuCanvas()
    {
        moveSpeed = 0;
        menuCanvas.enabled = true;
        playCanvas.enabled = false;
        gameOverCanvas.enabled = false;
    }

    void GameOver()
    {
        moveSpeed = 0;
        timerIsRunning = true;
        menuCanvas.enabled = false;
        playCanvas.enabled = false;
        gameOverCanvas.enabled = true;
        endScoreText.text = count.ToString() + "  <sprite=0>";
        if (gameOverCanvas.enabled == true)
        {
            AplauseTimer();
        }
        
    }

    void ActivateAdsButton()
    {
        if (menuCanvas.enabled == true)
        {
            if (lives <= 1)
            {
                adsButton.gameObject.SetActive(true);
            }
            else if (lives > 1)
            {
                adsButton.gameObject.SetActive(false);
                SetText();
            }
        }
    }

    void SavePlayer()
    {
        ES3.Save("highScore", highScore);
    }

    void LoadPlayer()
    {
        if (ES3.KeyExists("highScore"))
            highScore = ES3.Load<int>("highScore");
    }

    public void ExitButton()
    {
        SavePlayer();
        Application.Quit();
    }

    public void PlayButton()
    {
        moveSpeed = 8;
        menuCanvas.enabled = false;
        playCanvas.enabled = true;
        gameOverCanvas.enabled = false;
    }
}
