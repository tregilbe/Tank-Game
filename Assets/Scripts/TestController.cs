﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public TankMotor motor;
    // Start is called before the first frame update
    void Start()
    {
        // Demonstrate GameManager functionality
        Debug.Log(GameManager.Instance.demoNumber);
    }

    // Update is called once per frame
    void Update()
    {
        motor.Move(3);
        motor.Rotate(180);
    }
}
