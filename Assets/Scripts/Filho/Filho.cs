using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filho : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(player.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
