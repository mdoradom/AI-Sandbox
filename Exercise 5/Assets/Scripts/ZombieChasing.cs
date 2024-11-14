using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using UnityEngine.AI;

[Action("ZombieActions/Chasing")]
[Help("Makes the zombie chase the player.")]
public class ZombieChasing : BasePrimitiveAction
{
    private NavMeshAgent agent;
    private Transform player;
    private Camera frustum;
    private LayerMask mask;

    public override void OnStart()
    {
        // Inicializamos las variables necesarias
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GameObject.Find("Zombie").GetComponent<NavMeshAgent>();
        frustum = GameObject.Find("ZombieCamera").GetComponent<Camera>(); // Asume que tienes una c�mara del zombie
        mask = LayerMask.GetMask("Player"); // Asume que el jugador tiene el layer "Player"
    }

    public override TaskStatus OnUpdate()
    {
        // Establecer la nueva posici�n objetivo
        agent.SetDestination(player.position);

        // Verificar si el jugador est� dentro del campo de visi�n del zombie
        Vector3 playerPositionInViewport = frustum.WorldToViewportPoint(player.position);

        // Si el jugador est� fuera de la vista del frustum o demasiado lejos
        if (!(playerPositionInViewport.z > 0 &&
              playerPositionInViewport.x > 0 && playerPositionInViewport.x < 1 &&
              playerPositionInViewport.y > 0 && playerPositionInViewport.y < 1))
        {
            // El zombie cambia al estado de Idle si el jugador se aleja
            return TaskStatus.FAILED; // Consideramos que el estado de Chase ha fallado porque el jugador ya no es visible
        }

        // Si el jugador sigue a la vista, seguimos persigui�ndolo
        if (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            return TaskStatus.RUNNING; // El zombie sigue persiguiendo al jugador
        }

        // Si el zombie ha alcanzado el objetivo (el jugador), se detiene (puedes agregar un ataque o algo aqu�)
        return TaskStatus.COMPLETED; // El estado de Chasing se complet�
    }
}
