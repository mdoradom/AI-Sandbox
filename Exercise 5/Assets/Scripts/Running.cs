using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using UnityEngine.AI;

[Action("MyActions/Running")]
[Help("Makes the player run away continuously in a forward direction.")]
public class Running : BasePrimitiveAction
{
    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 fleeDirection;

    [InParam("Zombie")]
    [Help("The zombie chasing the player.")]
    public GameObject zombie;

    public override void OnStart()
    {
        player = GameObject.Find("Player");
        agent = player.GetComponent<NavMeshAgent>();

        if (zombie != null)
        {
            // Calcular la direcci�n de huida: opuesta a la direcci�n del zombie
            fleeDirection = (player.transform.position - zombie.transform.position).normalized;
        }
        else
        {
            // Si no hay un zombie asignado, huye en una direcci�n fija hacia adelante
            fleeDirection = player.transform.forward;
        }

        // Asegurar que el agente se mueva en la direcci�n inicial
        Vector3 fleePosition = player.transform.position + fleeDirection * 100.0f; // Corre hacia adelante
        agent.SetDestination(fleePosition);
    }

    public override TaskStatus OnUpdate()
    {
        // Continuar movi�ndose en la misma direcci�n
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Recalcula una posici�n m�s lejos en la misma direcci�n de huida
            Vector3 fleePosition = agent.transform.position + fleeDirection * 100.0f;
            agent.SetDestination(fleePosition);
        }

        return TaskStatus.RUNNING;
    }
}
