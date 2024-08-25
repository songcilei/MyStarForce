using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Transform target;
    void Start()
    {
        _agent = this.GetComponent<NavMeshAgent>();
        _agent.Warp(this.transform.position);    
        _agent.SetDestination(target.position);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
