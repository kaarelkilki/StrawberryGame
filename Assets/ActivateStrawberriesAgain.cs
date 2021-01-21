using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateStrawberriesAgain : MonoBehaviour
{
    public float sec = 14f;
 
    void Update()
    {
        if (transform.GetChild(0).gameObject.activeInHierarchy == false)
        {
            StartCoroutine(LateCall());
        }
        if (transform.GetChild(1).gameObject.activeInHierarchy == false)
        {
            StartCoroutine(LateCall1());
        }
    }

    IEnumerator LateCall()
    {

        yield return new WaitForSeconds(sec);

        transform.GetChild(0).gameObject.SetActive(true);
    }
    
    IEnumerator LateCall1()
    {

        yield return new WaitForSeconds(sec);

        transform.GetChild(1).gameObject.SetActive(true);
    }
}
