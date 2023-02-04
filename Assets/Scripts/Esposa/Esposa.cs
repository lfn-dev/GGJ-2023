using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Esposa : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask oqueEChao, oqueEPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    bool jaACasou;

    public float sightRange, attackRange;
    public bool playerIsInSigthRange, playerInAttackRange;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerIsInSigthRange = Physics.CheckSphere(transform.position, sightRange, oqueEPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, oqueEPlayer);

        if (!playerIsInSigthRange && !playerInAttackRange) Patroling();
        if (playerIsInSigthRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerIsInSigthRange) Casaram();
    }

    private void Patroling()
    {
        if (!walkPointSet) SceareachWalkingPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkingPoint = transform.position - walkPoint;

        if (distanceToWalkingPoint.magnitude < 1f)
            walkPointSet = false;

    }

    public void SceareachWalkingPoint()
    {
        float randomz = Random.Range(-walkPointRange, walkPointRange);
        float randomx = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomz);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, oqueEChao))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void Casaram()
    {

    }
}
