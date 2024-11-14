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
            // Calcular la dirección de huida: opuesta a la dirección del zombie
            fleeDirection = (player.transform.position - zombie.transform.position).normalized;
        }
        else
        {
            // Si no hay un zombie asignado, huye en una dirección fija hacia adelante
            fleeDirection = player.transform.forward;
        }

        // Asegurar que el agente se mueva en la dirección inicial
        Vector3 fleePosition = player.transform.position + fleeDirection * 100.0f; // Corre hacia adelante
        agent.SetDestination(fleePosition);
    }

    public override TaskStatus OnUpdate()
    {
        // Continuar moviéndose en la misma dirección
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Recalcula una posición más lejos en la misma dirección de huida
            Vector3 fleePosition = agent.transform.position + fleeDirection * 100.0f;
            agent.SetDestination(fleePosition);
        }

        return TaskStatus.RUNNING;
    }
}
