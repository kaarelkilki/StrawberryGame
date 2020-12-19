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
    public Button left;
    public Button right;
    public TMP_Text scoreText;
    public TMP_Text levelText;

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
    }
    void Update()
    {
        //moves player left and right
        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, 0).normalized;

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
}
