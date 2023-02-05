using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Filho : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public int filho;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(player.position);
        filho += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
