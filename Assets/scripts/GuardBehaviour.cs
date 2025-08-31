using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardBehaviour : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform[] players;
    public Animator animator;
    public float detectionRange = 10f;
    public float fieldOfView = 120f;
    public LayerMask obstructionMask;

    private NavMeshAgent agent;
    private int currentIndex = 0;
    private bool isWaiting = false;
    private bool isChasing = false;
    public bool canInteract = true;
    private Transform chaseTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentIndex].position);
            animator.SetBool("Walking", true);
        }
    }

    void Update()
    {
        if (!isChasing)
        {
            LookForPlayers();
            agent.speed = 3.5f;
            if (!agent.pathPending && agent.remainingDistance < 0.5f && !isWaiting)
            {
                StartCoroutine(WaitAtWaypoint());
            }
        }
        else
        {
            if (chaseTarget != null)
            {
                agent.SetDestination(chaseTarget.position);
                agent.speed = 10.0f;
            }
        }

        animator.SetBool("Walking", agent.velocity.magnitude > 0.1f && agent.velocity.magnitude < 3.6f);
        animator.SetBool("Running", agent.velocity.magnitude > 3.6f);
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        agent.isStopped = true;

        yield return new WaitForSeconds(3f);

        currentIndex = (currentIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentIndex].position);
        agent.isStopped = false;
        isWaiting = false;
    }

    void LookForPlayers()
    {
        Vector3 guardEye = transform.position + Vector3.up * 1.5f;

        foreach (Transform player in players)
        {
            if (player == null) continue;

            // Try to get collider center, or offset if missing
            Vector3 playerCenter;
            Collider playerCollider = player.GetComponent<Collider>();
            if (playerCollider != null)
                playerCenter = playerCollider.bounds.center;
            else
                playerCenter = player.position + Vector3.up * 1.0f;

            Vector3 dirToPlayer = playerCenter - guardEye;
            float angle = Vector3.Angle(transform.forward, dirToPlayer);

            if (dirToPlayer.magnitude < detectionRange && angle < fieldOfView * 0.5f)
            {
                Ray ray = new Ray(guardEye, dirToPlayer.normalized);
                if (Physics.Raycast(ray, out RaycastHit hit, detectionRange, obstructionMask))
                {
                    if (hit.transform == player)
                    {
                        PlayerController playerscript = hit.transform.GetComponent<PlayerController>();
                        if (playerscript != null && playerscript.hidden == false)
                        {
                            isChasing = true;
                            chaseTarget = player;
                            break;
                        }
                    }
                }
            }
        }
    }
}
