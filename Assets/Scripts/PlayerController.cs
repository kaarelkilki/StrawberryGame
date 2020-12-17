using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8;

    [Range(50, 250)]
    public float rotationSpeed;
    //private Vector3 moveDir;
    
    public Button left;
    public Button right;
    Vector3 m_EulerAngleVelocity;
    private float rotation;

    private int count;

    public Text countText;

    void Start()
    {
       
        count = 0;
        m_EulerAngleVelocity = new Vector3(0, 0, 45);

    }
    void Update()
    {
        //moveDir = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical")).normalized;
    }
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + this.transform.forward * moveSpeed * Time.deltaTime);

        left.onClick.AddListener(TurnLeft);
     
    }
    public void TurnLeft()
    {
        Debug.Log("Left");
        
        Vector3 rotationY = Vector3.up * rotation * rotationSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(rotationY);
        Quaternion targetRotation = GetComponent<Rigidbody>().rotation * deltaRotation;
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(GetComponent<Rigidbody>().rotation, targetRotation, 50f * Time.fixedDeltaTime));


        //StartCoroutine(RotateLeft(Vector3.forward, -45, 0.5f));
        //Vector3 rotationVector = new Vector3(0, 30, 0);
        //Quaternion rotation = Quaternion.Euler(rotationVector);
        //GetComponent<Rigidbody>().transform.Rotate(0, 45, 0, Space.Self);
        //Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        //GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
        }
    }
    IEnumerator RotateLeft(Vector3 axis, float angle, float duration = 1.0f)
    {
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to;
    }
}
