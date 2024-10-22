using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // Mover al jugador a una posición aleatoria en la NavMesh al inicio
        MoveToRandomPosition();
    }

    void Update()
    {
        // Verificar si el agente ha llegado a su destino
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                // Mover al jugador a una nueva posición aleatoria
                MoveToRandomPosition();
            }
        }
    }

    void MoveToRandomPosition()
    {
        Vector3 randomPosition = GetRandomPositionOnNavMesh();
        if (randomPosition != Vector3.zero)
        {
            agent.SetDestination(randomPosition);
        }
    }

    Vector3 GetRandomPositionOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 50f; // Radio de búsqueda
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 50f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return Vector3.zero;
    }
}