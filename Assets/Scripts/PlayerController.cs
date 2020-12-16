using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float thrust;
    //public float forwardForce = 15;
    public float moveSpeed = 15;
    private Vector3 moveDir;
    public Button left;
    public Button right;

    private int count;

    public Text countText;
    void Start()
    {
        count = 0;
    }
    void Update()
    {
        moveDir = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical")).normalized;
    }
    void FixedUpdate()
    {
        //rb.AddRelativeForce(Vector3.forward * thrust);
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
        
        //rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        //if(Input.GetKey("d"))
        //{
        //    rb.AddForce(500 * Time.deltaTime, 0, 0);
        //}
        //if (Input.GetKey("a"))
        //{
        //    rb.AddForce(-500 * Time.deltaTime, 0, 0);
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
        }
    }
}
