using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingEnemy : MonoBehaviour
{

    public Transform[] patrolPoints;
    int patrolPointID = 0;
    Vector3 patrolPointDistance;

    public GameObject player;
    int attackRange = 20;

    public float radius;
    [Range(0,360)]
    public float angle;

    public LayerMask playerMask;
    public LayerMask obstructionMask;

    bool canSeePlayer = false;

    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // Finds the Objects NavMeshAgent
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Gets the enemy to initially go to the first patrol point, then starts the FOV Routine
        UpdateDestination();
        StartCoroutine(FOVRoutine());
    }

    void Update()
    {
        // Gets the enemy to detect if the current targeted patrolpoint object is close and makes it change to the next patrol point
        if (Vector3.Distance(transform.position, patrolPointDistance) < 10)
        {
            ChangePatrolPointID();
            UpdateDestination();
        }

        // Makes the enemy attack the player if it is within range and is able to have a clean view of the player
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange && canSeePlayer)
        {
            navMeshAgent.destination = player.transform.position;
        }
        else // If the player gets out of sight of the enemy it changes its destination back to the current patrol point it was chasing before attacking
        {
            UpdateDestination();
        }
    }

    private void UpdateDestination()
    {
        // Sets the enemy's destination to the current patrol point
        patrolPointDistance = patrolPoints[patrolPointID].position;
        navMeshAgent.destination = patrolPointDistance;
    }

    private void ChangePatrolPointID()
    {
        // Updates the the current patrol point to the next in the list or sets it to 0 if reached the end
        patrolPointID++;
        if (patrolPointID == patrolPoints.Length)
        {
            patrolPointID = 0;
        }
    }

    private IEnumerator FOVRoutine()
    {
        // Runs the field of view check only 5 times a second instead of every frame, to help with unnessecary hit to performance
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    // Checks if the enemy is able to see the player using spherecast as the field of view, while checking for obstructions using a raycast to the player
    private void FieldOfViewCheck()
    {
        // Creates an array of colliders which contains all colliders within the overlapping sphere and belongs to the playerMask layermask
        Collider[] inViewColliders = Physics.OverlapSphere(transform.position, radius, playerMask);

        if (inViewColliders.Length != 0)
        {
            // If the player is found, it gets the players transform and creates a direction vector from the enemy to the player
            Transform target = inViewColliders[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // Then it checks how big the angle between the direction vector and the enemy's forward vector is and continues if the angle is less than the set FOV angle
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                // Finds the distance from the enemy and the player
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                // Then casts a RayCast from the enemy to the player and checks if it is not hitting an obstruction, then it set a bool to true else it is set to false
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else canSeePlayer = false;
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }
}
