using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveAIController : MonoBehaviour
{
    public enum AIPersonality
    {
        Aggressive,
    }

    public enum AIState
    {    
        Attack,
    }

    public AIPersonality currentPersonality;
    private AIState currentAIState;
    public AIState previousAIState;
    private TankData data;
    private TankMotor motor;
    public Transform target;
    private Transform tf;

    public float avoidanceTime = 2f;
    private float exitTime;

    public enum AvoidStage { notAvoiding, rotateUntilCanMove, moveForSeconds }
    public AvoidStage avoidanceStage;

    public float stateEnterTime;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
        tf = GetComponent<Transform>();
    }

    private void Update()
    {
        //(!(avoidanceStage == AvoidStage.notAvoiding))
        if (avoidanceStage != AvoidStage.notAvoiding)
        {
            Avoid();
        }
        else
        {
            Chase();
        }

        switch (currentPersonality)
        {
            case AIPersonality.Aggressive:
                currentAIState = AIState.Attack;
                AggressiveTankFSM();
                break;

            default:
                Debug.LogWarning("Unimplemented personality");
                break;
        }
    }

    public void ChangeState(AIState newState)
    {
        // Save the previous state.
        previousAIState = currentAIState;

        // Change to new state.
        currentAIState = newState;

        // Save the time we changed states at.
        stateEnterTime = Time.time;
    }

    private void AggressiveTankFSM()
    {
        switch (currentAIState)
        {
            case AIState.Attack:              
                break;
        }
    }


    void Avoid()
    {
        if (avoidanceStage == AvoidStage.rotateUntilCanMove)
        {
            motor.Rotate(-1 * data.rotateSpeed);

            if (CanMove(data.moveSpeed))
            {
                Debug.Log("Stage 1 done");
                avoidanceStage = AvoidStage.moveForSeconds;
                exitTime = avoidanceTime;
            }
        }
        else if (avoidanceStage == AvoidStage.moveForSeconds)
        {
            if (CanMove(data.moveSpeed))
            {
                Debug.Log("Stage 2 done");
                exitTime -= Time.deltaTime;
                motor.Move(data.moveSpeed);

                if (exitTime <= 0)
                {
                    avoidanceStage = AvoidStage.notAvoiding;
                }
            }
            else
            {
                avoidanceStage = AvoidStage.rotateUntilCanMove;
            }
        }
    }

    void Chase()
    {
        motor.RotateTowards(target.position, data.rotateSpeed);
        if (CanMove(data.moveSpeed))
        {
            //move if can move
            //if (motor.RotateTowards(target.position, data.rotateSpeed))
            //{
                // Do nothing
           // }
            //else
            //{
                motor.Move(data.moveSpeed);
            Shoot();
           // }
        }
        else
        {
            avoidanceStage = AvoidStage.rotateUntilCanMove;
            Debug.Log("Chase Loop Error");
        }

    }

    void Shoot()
    {
        if (data.timeBtwShots <= 0)
        {
            // Instantiate the vullet prefab
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(data.Bullet, data.shotPoint.transform.position, data.shotPoint.transform.rotation) as GameObject;

            // Retrieve the Rigidbody of the bullet 
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            // add force to the bullet
            Temporary_RigidBody.AddForce(transform.forward * data.bulletSpeed);

            // Clean up, set timer till the bullet is destroyed
            Destroy(Temporary_Bullet_Handler, data.bulletLifetime);

            data.timeBtwShots = data.startTimeBtwShots;
        }
        else
        {
            data.timeBtwShots -= Time.deltaTime;
        }
    }

    public bool CanMove(float speed)
    {

        // Raycast forward
        RaycastHit hit;

        if (Physics.Raycast(tf.position, tf.forward, out hit, speed))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                return false;
            }
        }
        return true;
    }

}
