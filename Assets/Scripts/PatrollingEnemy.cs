using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingEnemy : MonoBehaviour
{

    public Transform[] patrolPoints;
    int patrolPointID = 0;
    Vector3 target;

    bool attacking = false;
    public Transform player;
    int attackRange = 20;

    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        UpdateDestination();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 10)
        {
            ChangePatrolPointID();
            UpdateDestination();
        }

        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            attacking = true;
            navMeshAgent.destination = player.position;
        }
        else
        {
            attacking = false;
            UpdateDestination();
        }
    }

    private void UpdateDestination()
    {
        target = patrolPoints[patrolPointID].position;
        navMeshAgent.destination = target;
    }

    private void ChangePatrolPointID()
    {
        patrolPointID++;
        if (patrolPointID == patrolPoints.Length)
        {
            patrolPointID = 0;
        }
    } 
}
