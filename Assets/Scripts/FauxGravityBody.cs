using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class FauxGravityBody : MonoBehaviour
{
    public FauxGravityAtractor attractor;
    private Transform myTransform;
    void Start()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().useGravity = false;
        myTransform = transform;
    }
    void Update()
    {
        attractor.Attract(myTransform);
    }
}
