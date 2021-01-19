using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Mirror;

public class MultiplayerControler : NetworkBehaviour
{
    // https://www.youtube.com/watch?v=8VVgIjWBXks at 21.06

    public float moveSpeed;
    public AudioSource collectStrawberry;
    public TMP_Text scoreText;
    public TMP_Text timeText;

    public int count;
    private Vector3 moveDir;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = 8;
        //SetScoreText();
    }

    void Update()
    {
        PlayerLeftRight();
    }

    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);

            // moves the player in it's front direction
            rb.MovePosition(rb.position + this.transform.forward * moveSpeed * Time.deltaTime);
        }
    }

    void SetScoreText()
    {
        scoreText.text = count.ToString() + "  <sprite=0>";
    }

    void PlayerLeftRight()
    {
        if (isLocalPlayer)
        {
            //player movement in Android device
        #if UNITY_ANDROID
            
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.position.x > (Screen.width / 2) & touch.phase == TouchPhase.Began)
                {
                    moveDir = new Vector3(1, 0, 0).normalized;
                }

                if (touch.position.x < (Screen.width / 2) & touch.phase == TouchPhase.Began)
                {
                    moveDir = new Vector3(-1, 0, 0).normalized;
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    moveDir = new Vector3(0, 0, 0).normalized;
                }
            }
        #endif

            //moves player left and right on computer
        #if UNITY_EDITOR
                moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, 0).normalized;
        #endif
        }
    }
}
