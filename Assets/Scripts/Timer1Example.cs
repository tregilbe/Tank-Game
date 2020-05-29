using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer1Example : MonoBehaviour
{
    public float timerDelay = 3.0f;
    private float timeUntilNextEvent = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeUntilNextEvent > 0)
        {
            timeUntilNextEvent -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Timer1 has ended.");
            timeUntilNextEvent = timerDelay;
        }
    }
}
