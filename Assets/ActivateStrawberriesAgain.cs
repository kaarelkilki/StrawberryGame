using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateStrawberriesAgain : MonoBehaviour
{
    public float sec = 1f;
 
    void Start()
    {

    }
    void Update()
    {
        if (transform.GetChild(0).gameObject.ActiveInHierarchy == false)
        {
            StartCoroutine(LateCall());
        }
    }

    IEnumerator LateCall()
    {

        yield return new WaitForSeconds(sec);

        transform.GetChild(0).gameObject.SetActive(true);
        //Do Function here...
    }
}
