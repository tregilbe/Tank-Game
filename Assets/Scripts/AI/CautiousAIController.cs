using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CautiousAIController : MonoBehaviour
{
    public enum AIPersonality
    {
        Cautious,
    }

    public enum AIState
    {
        Patrol,
        WaitForBackup,
        Advance,
        Attack,
        Flee
    }

    public AIPersonality currentPersonality;
    private AIState currentAIState;
    public AIState previousAIState;
    private TankData data;
    private TankMotor motor;
    public Transform target;
    private Transform tf;

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

    public enum AvoidStage { notAvoiding, rotateUntilCanMove, moveForSeconds }
    public AvoidStage avoidanceStage;

    public float stateEnterTime;

    private void Start()
    {
        GameManager.Instance.Enemies.Add(this.gameObject.GetComponent<TankData>());

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
        tf = GetComponent<Transform>();

        waypoints = GameManager.Instance.currentCautiousSpawnPoint.waypoints;
    }

    private void OnDestroy()
    {
        GameManager.Instance.Enemies.Remove(this.gameObject.GetComponent<TankData>());
    }

    private void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        switch (currentPersonality)
        {
            case AIPersonality.Cautious:
                CautiousTankFSM();
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

    private void CautiousTankFSM()
    {
        switch (currentAIState)
        {

            case AIState.Flee:
                if (avoidanceStage != AvoidStage.notAvoiding)
                {
                    Avoid();
                }
                else
                {
                    Flee();
                }
                break;
            case AIState.Patrol:
                Patrol();
                // Check for transitions
                // Should we flee?
                if (CheckForFlee())
                {
                    currentAIState = AIState.Flee; 
                }
                // Should we wait for backup?
                break;
            case AIState.Advance:
                break;
        }
    }

    private void Flee()
    {
        Vector3 vectorToTarget = target.position - tf.position;
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        vectorAwayFromTarget.Normalize();
        Vector3 fleePosition = vectorAwayFromTarget + tf.position;
        motor.RotateTowards(fleePosition, data.rotateSpeed);

        if (CanMove(data.moveSpeed))
        {
            motor.Move(data.moveSpeed);
        }
        else
        {
            avoidanceStage = AvoidStage.rotateUntilCanMove;
            Debug.Log("Flee Loop Error");
        }
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
        //(target.GetComponent<TankData>().health <= 50)
        if (data.currentHealth <= 50)
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
