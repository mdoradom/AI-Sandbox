using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using UnityEngine.AI;

[Action("MyActions/Wander")]
[Help("Makes the player wander randomly. If the player takes too long to reach the destination, a new point is assigned.")]
public class Wander : BasePrimitiveAction
{
    private NavMeshAgent agent;
    private Vector3 wanderPos;
    private float timer;
    private float maxTimeToReach = 8.0f;  // Tiempo máximo para llegar al destino antes de cambiar

    public override void OnStart()
    {
        GameObject player = GameObject.Find("Player");
        agent = player.GetComponent<NavMeshAgent>();

        // Inicializa el destino aleatorio
        SetRandomDestination();

        timer = 0f;  // Reinicia el temporizador
    }

    public override TaskStatus OnUpdate()
    {
        timer += Time.deltaTime;

        // Si el agente ha tardado más de 8 segundos en llegar a su destino, cambiar a otro punto aleatorio
        if (timer > maxTimeToReach)
        {
            SetRandomDestination();
            timer = 0f;  // Reinicia el temporizador al cambiar el destino
        }

        // Si el agente aún está pendiente de la ruta o no ha llegado al destino, continúa la tarea de "Wander"
        if (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            return TaskStatus.RUNNING;
        }

        // Si el agente ha llegado a su destino, la tarea se considera completada
        return TaskStatus.COMPLETED;
    }

    // Función para establecer un nuevo destino aleatorio
    private void SetRandomDestination()
    {
        GameObject player = GameObject.Find("Player");
        wanderPos = player.transform.position + Random.insideUnitSphere * 50.0f;
        wanderPos.y = player.transform.position.y;  // Mantener la misma altura que el jugador
        agent.SetDestination(wanderPos);
    }
}
