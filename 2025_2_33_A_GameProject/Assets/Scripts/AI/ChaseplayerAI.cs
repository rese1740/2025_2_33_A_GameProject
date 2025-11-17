using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseplayerAI : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 50.0f;
    public float attackRange = 2.0f;

    private NavMeshAgent agent;
    private float distanceToPlayer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }
}
