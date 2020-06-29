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
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
        tf = GetComponent<Transform>();
    }

    private void Update()
    {
        if (!(avoidanceStage == AvoidStage.notAvoiding))
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
                Chase();
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
                avoidanceStage = AvoidStage.moveForSeconds;
                exitTime = avoidanceTime;
            }
        }
        else if (avoidanceStage == AvoidStage.moveForSeconds)
        {
            if (CanMove(data.moveSpeed))
            {
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
        if (CanMove(data.moveSpeed))
        {
            //move if can move
            if (motor.RotateTowards(target.position, data.rotateSpeed))
            {
                // Do nothing
            }
            else
            {
                motor.Move(data.moveSpeed);
            }

        }
        else
        {
            avoidanceStage = AvoidStage.rotateUntilCanMove;
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
