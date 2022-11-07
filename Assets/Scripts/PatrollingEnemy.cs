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
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        UpdateDestination();
        StartCoroutine(FOVRoutine());
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, patrolPointDistance) < 10)
        {
            ChangePatrolPointID();
            UpdateDestination();
        }

        if (Vector3.Distance(transform.position, player.transform.position) < attackRange && canSeePlayer)
        {
            navMeshAgent.destination = player.transform.position;
        }
        else
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

    private void FieldOfViewCheck()
    {
        // Checks if the enemy is able to see the player using spherecast as the field of view, while checking for obstructions using a raycast to the player
        Collider[] inViewColliders = Physics.OverlapSphere(transform.position, radius, playerMask);

        if (inViewColliders.Length != 0)
        {
            // Debug.Log("I Found The Player");
            Transform target = inViewColliders[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                // Debug.Log("I See The Player");
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

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
