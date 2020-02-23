using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Awake()
    {
        //animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
}
