using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class ScoutAIController : MonoBehaviour
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

    public Hearing hearing;
    public Vision vision;

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
        GameManager.Instance.Enemies.Add(this.gameObject.GetComponent<TankData>());

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
        tf = GetComponent<Transform>();
        EC = GetComponent<EnemyController>();
        hearing = GetComponent<Hearing>();
        vision = GetComponent<Vision>();

        waypoints = GameManager.Instance.currentScoutSpawnPoint.waypoints;
    }
    private void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        switch (currentPersonality)
        {
            case AIPersonality.Turret:
                TurretTankFSM();
                break;

            default:
                Debug.LogWarning("Unimplemented personality");
                break;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.Enemies.Remove(this.gameObject.GetComponent<TankData>());
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
            case AIState.Patrol:

                Patrol();

                if (hearing.CanHear(target.gameObject) == true)
                {
                    Aim();

                    if (vision.CanSee(target.gameObject) == true)
                    {
                        currentAIState = AIState.Turret;
                    }
                    else
                    {
                        Patrol();
                    }
                }

                break;

            case AIState.Turret:

                if (vision.CanSee(target.gameObject) == true)
                {
                    Aim();
                    Shoot();
                }
                else
                {
                    currentAIState = AIState.Patrol;
                }
                
                break;
        }
    }

    void Aim()
    {
        motor.RotateTowards(target.position, data.rotateSpeed);
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
        if (target.GetComponent<TankData>().currentHealth <= 50)
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