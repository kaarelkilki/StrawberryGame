using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateStrawberriesAgain : MonoBehaviour
{
    public float sec = 14f;
    private List<float> timers = new List<float>();

    private void Update()
    {
        for (int i = 0; i < transform.childCount; ++i)
            ProcessChild(i);
    }

    private void ProcessChild(int childIndex)
    {
        GameObject child = transform.GetChild(childIndex).gameObject;

        if (childIndex >= timers.Count)
            timers.Add(sec);

        if (child.activeInHierarchy == false)
            timers[childIndex] -= Time.deltaTime;

        if (timers[childIndex] < 0)
        {
            child.SetActive(true);
            timers[childIndex] = sec;
        }
    }
}
