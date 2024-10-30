using UnityEngine;
using UnityEngine.AI;

public abstract class State
{
    protected ZombieMovement zombie;

    public State(ZombieMovement zombie)
    {
        this.zombie = zombie;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class ZombieIdleState : State
{
    public ZombieIdleState(ZombieMovement zombie) : base(zombie) { }

    public override void Enter()
    {
        zombie.animator.SetBool("isRunning", false);
    }

    public override void Update()
    {
        Vector3 playerPositionInViewport = zombie.frustum.WorldToViewportPoint(zombie.player.position);

        if (playerPositionInViewport.z > 0 &&
            playerPositionInViewport.x > 0 && playerPositionInViewport.x < 1 &&
            playerPositionInViewport.y > 0 && playerPositionInViewport.y < 1)
        {
            Vector3 direction = (zombie.player.position - zombie.frustum.transform.position).normalized;
            Ray ray = new Ray(zombie.frustum.transform.position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, zombie.frustum.farClipPlane, zombie.mask))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    zombie.ChangeState(new ZombieChasingState(zombie));
                }
            }
        }
    }

    public override void Exit() { }
}

public class ZombieChasingState : State
{
    public ZombieChasingState(ZombieMovement zombie) : base(zombie) { }

    public override void Enter()
    {
        zombie.animator.SetBool("isRunning", true);
    }

    public override void Update()
    {
        zombie.agent.SetDestination(zombie.player.position);

        Vector3 playerPositionInViewport = zombie.frustum.WorldToViewportPoint(zombie.player.position);

        if (!(playerPositionInViewport.z > 0 &&
              playerPositionInViewport.x > 0 && playerPositionInViewport.x < 1 &&
              playerPositionInViewport.y > 0 && playerPositionInViewport.y < 1))
        {
            zombie.ChangeState(new ZombieIdleState(zombie));
        }
    }

    public override void Exit() { }
}

public class ZombieMovement : MonoBehaviour
{
    public Transform player;
    public Camera frustum;
    public LayerMask mask;
    public NavMeshAgent agent;
    public Animator animator;

    private State currentState;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        ChangeState(new ZombieIdleState(this));
    }

    void Update()
    {
        currentState.Update();
    }

    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    void DrawFrustum(Camera cam)
    {
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        Vector3[] frustumCorners = new Vector3[4];
        frustumCorners[0] = cam.transform.TransformPoint(new Vector3(-halfWidth, -halfHeight, cam.nearClipPlane));
        frustumCorners[1] = cam.transform.TransformPoint(new Vector3(halfWidth, -halfHeight, cam.nearClipPlane));
        frustumCorners[2] = cam.transform.TransformPoint(new Vector3(halfWidth, halfHeight, cam.nearClipPlane));
        frustumCorners[3] = cam.transform.TransformPoint(new Vector3(-halfWidth, halfHeight, cam.nearClipPlane));

        Debug.DrawLine(frustumCorners[0], frustumCorners[1], Color.blue);
        Debug.DrawLine(frustumCorners[1], frustumCorners[2], Color.blue);
        Debug.DrawLine(frustumCorners[2], frustumCorners[3], Color.blue);
        Debug.DrawLine(frustumCorners[3], frustumCorners[0], Color.blue);

        frustumCorners[0] = cam.transform.TransformPoint(new Vector3(-halfWidth, -halfHeight, cam.farClipPlane));
        frustumCorners[1] = cam.transform.TransformPoint(new Vector3(halfWidth, -halfHeight, cam.farClipPlane));
        frustumCorners[2] = cam.transform.TransformPoint(new Vector3(halfWidth, halfHeight, cam.farClipPlane));
        frustumCorners[3] = cam.transform.TransformPoint(new Vector3(-halfWidth, halfHeight, cam.farClipPlane));

        Debug.DrawLine(frustumCorners[0], frustumCorners[1], Color.blue);
        Debug.DrawLine(frustumCorners[1], frustumCorners[2], Color.blue);
        Debug.DrawLine(frustumCorners[2], frustumCorners[3], Color.blue);
        Debug.DrawLine(frustumCorners[3], frustumCorners[0], Color.blue);

        Debug.DrawLine(cam.transform.TransformPoint(new Vector3(-halfWidth, -halfHeight, cam.nearClipPlane)), cam.transform.TransformPoint(new Vector3(-halfWidth, -halfHeight, cam.farClipPlane)), Color.blue);
        Debug.DrawLine(cam.transform.TransformPoint(new Vector3(halfWidth, -halfHeight, cam.nearClipPlane)), cam.transform.TransformPoint(new Vector3(halfWidth, -halfHeight, cam.farClipPlane)), Color.blue);
        Debug.DrawLine(cam.transform.TransformPoint(new Vector3(halfWidth, halfHeight, cam.nearClipPlane)), cam.transform.TransformPoint(new Vector3(halfWidth, halfHeight, cam.farClipPlane)), Color.blue);
        Debug.DrawLine(cam.transform.TransformPoint(new Vector3(-halfWidth, halfHeight, cam.nearClipPlane)), cam.transform.TransformPoint(new Vector3(-halfWidth, halfHeight, cam.farClipPlane)), Color.blue);
    }

    public void OnPlayerDetected()
    {
        Debug.Log("Received player detected message");
        ChangeState(new ZombieChasingState(this));
    }
}