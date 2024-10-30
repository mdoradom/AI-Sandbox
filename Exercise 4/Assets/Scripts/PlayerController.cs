using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private IState currentState;

    public NavMeshAgent Agent => agent;
    public Animator Animator => animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // Initialize the FSM with the Idle state
        SetState(new IdleState(this));
    }

    private void Update()
    {
        currentState.Update();
    }

    public void SetState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter();
    }

    public void MoveToRandomPosition()
    {
        Vector3 randomPosition = GetRandomPositionOnNavMesh();
        if (randomPosition != Vector3.zero)
        {
            agent.SetDestination(randomPosition);
            animator.SetBool("isRunning", true); // Set isRunning to true when moving
            SetState(new MovingState(this));
        }
    }

    private Vector3 GetRandomPositionOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 50f; // Search radius
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 50f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return Vector3.zero;
    }

    public void UpdateAnimation(bool isRunning)
    {
        animator.SetBool("isRunning", isRunning);
    }
}

public interface IState
{
    void Enter();
    void Update();
    void Exit();
}

public class IdleState : IState
{
    private PlayerController playerController;

    public IdleState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {
        playerController.UpdateAnimation(false);
    }

    public void Update()
    {
        if (!playerController.Agent.pathPending && playerController.Agent.remainingDistance <= playerController.Agent.stoppingDistance)
        {
            if (!playerController.Agent.hasPath || playerController.Agent.velocity.sqrMagnitude == 0f)
            {
                playerController.MoveToRandomPosition();
            }
        }
    }

    public void Exit()
    {
        // No exit logic needed for Idle state
    }
}

public class MovingState : IState
{
    private PlayerController playerController;

    public MovingState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {
        playerController.UpdateAnimation(true);
    }

    public void Update()
    {
        if (!playerController.Agent.pathPending && playerController.Agent.remainingDistance <= playerController.Agent.stoppingDistance)
        {
            if (!playerController.Agent.hasPath || playerController.Agent.velocity.sqrMagnitude == 0f)
            {
                playerController.SetState(new IdleState(playerController));
            }
        }
    }

    public void Exit()
    {
        // No exit logic needed for Moving state
    }
}