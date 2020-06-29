using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAIController : MonoBehaviour
{
    public enum AIPersonality
    {
        Turret
    }
    public enum AIState
    { 
        Patrol,
        Turret
    }

    public AIPersonality currentPersonality;
    private AIState currentAIState;
    public AIState previousAIState;
    private TankData data;
    private TankMotor motor;
    public Transform target;
    private Transform tf;
    public EnemyController EC;

    public Transform[] waypoints;
    public float closeEnough = 1.0f;
    public int currentWaypoint = 0;
    public enum LoopType
    {
        Stop,
        Loop,
        PingPong
    };

    public LoopType loopType;
    private bool isPatrolForward = true;

    public float avoidanceTime = 2f;
    private float exitTime;

    public float stateEnterTime;

    private void Start()
    {
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
        tf = GetComponent<Transform>();
        EC = GetComponent<EnemyController>();
    }
    private void Update()
    {
        TurretMode();

        switch (currentPersonality)
        {
            case AIPersonality.Turret:
                currentAIState = AIState.Turret;
                TurretTankFSM();
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

    private void TurretTankFSM()
    {
        switch (currentAIState)
        {
            case AIState.Turret:
                TurretMode();
                break;
        }
    }

    private void TurretMode()
    {
        Vector3 vectorToTarget = target.position - transform.position;
        motor.RotateTowards(vectorToTarget, data.rotateSpeed);
    }

    private void Patrol()
    {
        // Do the patrol behaviors
        if (motor.RotateTowards(waypoints[currentWaypoint].position, data.rotateSpeed))
        {
            // Do nothing
        }
        else
        {
            motor.Move(data.moveSpeed);
        }
        if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (closeEnough * closeEnough))
        {
            if (loopType == LoopType.Stop)
            {
                StopLoop();
            }
            else if (loopType == LoopType.PingPong)
            {
                PingPongLoop();
            }
            else if (loopType == LoopType.Loop)
            {
                LoopLoop();
            }
            else
            {
                Debug.LogWarning("[SampleAIController] Unimplemented loop type.");
            }
        }
    }

    private bool CheckForFlee()
    {
        if (target.GetComponent<TankData>().health <= 50)
        {
            return true;
        }

        return false;
    }

    private void LoopLoop()
    {
        if (currentWaypoint < waypoints.Length - 1)
        {
            currentWaypoint++;
        }
        else
        {
            currentWaypoint = 0;
        }
    }

    private void PingPongLoop()
    {
        if (isPatrolForward)
        {
            if (currentWaypoint < waypoints.Length - 1)
            {
                currentWaypoint++;
            }
            else
            {
                isPatrolForward = false;
                currentWaypoint--;
            }
        }
        else
        {
            if (currentWaypoint > 0)
            {
                currentWaypoint--;
            }
            else
            {
                isPatrolForward = true;
                currentWaypoint++;
            }
        }
    }

    private void StopLoop()
    {
        if (currentWaypoint < waypoints.Length - 1)
        {
            currentWaypoint++;
        }
    }
}