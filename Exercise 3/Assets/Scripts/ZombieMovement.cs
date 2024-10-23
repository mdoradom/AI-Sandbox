using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    private Transform player; // Referencia al Transform del jugador
    public Camera frustum; // Referencia a la cámara
    public LayerMask mask; // Máscara de capas para el raycast
    private NavMeshAgent agent; // Referencia al NavMeshAgent
    private Animator animator; // Referencia al Animator

    void Start()
    {
        // Buscar el GameObject del jugador por su tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        DrawFrustum(frustum);

        // Verifica si el jugador está dentro del frustum de visión de la cámara
        Vector3 playerPositionInViewport = frustum.WorldToViewportPoint(player.position);

        // Chequea si el jugador está dentro del rango de visión (en el frustum)
        if (playerPositionInViewport.z > 0 && 
            playerPositionInViewport.x > 0 && playerPositionInViewport.x < 1 &&
            playerPositionInViewport.y > 0 && playerPositionInViewport.y < 1)
        {
            // Lanza un raycast desde la cámara hacia el jugador para verificar si hay obstrucciones
            Vector3 direction = (player.position - frustum.transform.position).normalized;
            float distanceToPlayer = Vector3.Distance(frustum.transform.position, player.position);
            Ray ray = new Ray(frustum.transform.position, direction);

            // Dibuja el raycast en la vista de escena (debug)
            Debug.DrawRay(ray.origin, ray.direction * distanceToPlayer, Color.red);

            // Realiza el raycast para verificar si el jugador está realmente visible
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distanceToPlayer, mask))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // Mover el zombi hacia el jugador
                    agent.SetDestination(player.position);

                    // Actualizar el estado de la animación
                    animator.SetBool("isRunning", true);

                    // Llamar al método NotifyPlayerDetected del ZombieManager
                    ZombieManager manager = FindObjectOfType<ZombieManager>();
                    if (manager != null)
                    {
                        manager.NotifyPlayerDetected();
                    }
                }
                else
                {
                    // Si el jugador no está visible, detener la animación de caminar
                    animator.SetBool("isRunning", false);
                }
            }
        }
        else
        {
            // Si el jugador no está en el frustum, detener la animación de caminar
            animator.SetBool("isRunning", false);
        }
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

    // Método para manejar mensajes de detección de jugador
    public void OnPlayerDetected()
    {
        Debug.Log("Received player detected message");

        // Mover el zombi hacia el jugador
        agent.SetDestination(player.position);

        // Actualizar el estado de la animación
        animator.SetBool("isRunning", true);
    }
}