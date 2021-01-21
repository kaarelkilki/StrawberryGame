using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndlessRunner : MonoBehaviour
{
    public float moveSpeed;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public AudioSource collectStrawberry;

    public int count;
    public int highScore;
    public Rigidbody rb;

    private Vector3 moveDir;
    public int lives;

    [SerializeField]
    private Canvas menuCanvas;
    [SerializeField]
    private Canvas playCanvas;
    [SerializeField]
    private Button adsButton;

    Scene scene;

    void Start()
    {
        collectStrawberry.Stop();
        LoadPlayer();
        MenuCanvas();
        SetScoreText();
    }

    void Update()
    {
        PlayerLeftRight();
        SetMoveSpeed();
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

    void OnCollisionEnter(Collision coll)
    {
        if (lives < 1)
        {
            SavePlayer();
            SceneManager.LoadScene("EndlessRunner");
        }
        else
        {
            lives -= 1;
        }
    }

    void SetMoveSpeed()
    {
        if (playCanvas.enabled == true && count < 5)
        {
            moveSpeed = 8;
        }
        else if (count >= 5)
        {
            moveSpeed += count / 5;
        }
    }

    void SetScoreText()
    {
        scoreText.text = count.ToString() + " / " + highScore.ToString() + "  <sprite=0>";
        
        if (count >= highScore)
        {
            highScore = count;
            highScoreText.text = "<sprite=0>" + count.ToString();
        }
        else if (count < highScore)
        {
            highScoreText.text = "<sprite=0>" + highScore.ToString();
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

    void MenuCanvas()
    {
        moveSpeed = 0;
        menuCanvas.enabled = true;
        playCanvas.enabled = false;
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
        
    }
}
