using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
public class InputManager : MonoBehaviour
{
    private TankMotor motor;
    private TankData data;

    public int myScore;
    public int killPoints;
    public enum InputScheme { WASD, arrowKeys };
    public InputScheme input = InputScheme.WASD;
    // Start is called before the first frame update
    void Start()
    {
        motor = gameObject.GetComponent<TankMotor>();
        data = gameObject.GetComponent<TankData>();
        myScore = GetComponent<TankData>().Score;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();        
        // Shoot();        
    }

    void HandleInput()
    {
        switch (input)
        {
            case InputScheme.arrowKeys:
                // Handle movement
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    motor.Move(data.moveSpeed);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    motor.Move(-data.reverseSpeed);
                }
                else
                {
                    motor.Move(0);
                }

                // Handle rotation
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    motor.Rotate(data.rotateSpeed);
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    motor.Rotate(-data.rotateSpeed);
                }

                // Handle Shooting
                if (Input.GetKey(KeyCode.KeypadEnter))
                {
                    Shoot();
                }

                break;
            case InputScheme.WASD:
                // Handle Movement
                if (Input.GetKey(KeyCode.W))
                {
                    motor.Move(data.moveSpeed);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    motor.Move(-data.reverseSpeed);
                }
                else
                {
                    motor.Move(0);
                }

                // Handle rotation
                if (Input.GetKey(KeyCode.D))
                {
                    motor.Rotate(data.rotateSpeed);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    motor.Rotate(-data.rotateSpeed);
                }             
                // Handle Shooting
                if (Input.GetKey(KeyCode.Space))
                {
                    Shoot();
                }
                break;

            default:
                Debug.LogError("[InputManager] Undefined input scheme.");
                break;
        }
    }

    void Shoot()
    {
        if (data.timeBtwShots <= 0)
        {

                Instantiate(data.Bullet, data.shotPoint.position, data.shotPoint.rotation);

                //Instantiate(data.Bullet, data.shotPoint.position, transform.rotation);
                data.timeBtwShots = data.startTimeBtwShots;
            
        }
        else
        {
            data.timeBtwShots -= Time.deltaTime;
        }
    }
}