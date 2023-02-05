using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Filho : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
      
    public PlayerStats play;

    
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        play = GameObject.Find("Player").GetComponent<PlayerStats>();


        agent.SetDestination(player.position);
        play.filhos += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
