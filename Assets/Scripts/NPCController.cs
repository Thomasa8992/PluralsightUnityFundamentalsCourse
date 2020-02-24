using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float PatrolTime = 10f;
    public float AggroRange = 10;
    public Transform[] Waypoints;

    private int index;
    private float speed;
    private float agentSpeed;
    private Transform player;

    Animator animator;
    private NavMeshAgent navMeshAgent;

    private void Awake() {
        //animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        if(navMeshAgent != null) {
            agentSpeed = navMeshAgent.speed;
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
        index = Random.Range(0, Waypoints.Length);

        InvokeRepeating("tick", 0, 0.5f);

        if(Waypoints.Length > 0) {
            InvokeRepeating("patrol", 0, PatrolTime);
        }
    }

    private void patrol() {
        index = index == Waypoints.Length - 1 ? 0 : index + 1; 
    }

    private void tick() {
        navMeshAgent.destination = Waypoints[index].position;
        navMeshAgent.speed = agentSpeed / 2;

        if (player != null && Vector3.Distance(transform.position, player.position) < AggroRange) {
            navMeshAgent.destination = player.position;
            navMeshAgent.speed = agentSpeed;
        }
    }

}
