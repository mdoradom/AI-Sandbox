using Pada1.BBCore;
using Pada1.BBCore.Framework;
using UnityEngine;

namespace BBUnity.Conditions
{
    [Condition("MyConditions/isPlayerClose")]
    [Help("Checks if the player is within the zombie's field of view and line of sight.")]
    public class isPlayerClose : GOCondition
    {
        [InParam("player")]
        [Help("Reference to the player GameObject.")]
        public GameObject player;

        [InParam("zombieFrustum")]
        [Help("Camera representing the zombie's field of view.")]
        public Camera frustum;

        [InParam("viewMask")]
        [Help("Layer mask for line of sight detection.")]
        public LayerMask mask;

        public override bool Check()
        {
            if (player == null || frustum == null)
            {
                Debug.LogWarning("Player or frustum not assigned in isPlayerClose condition.");
                return false;
            }

            Vector3 playerPositionInViewport = frustum.WorldToViewportPoint(player.transform.position);

            // Check if player is within the field of view of the frustum
            if (playerPositionInViewport.z > 0 &&
                playerPositionInViewport.x > 0 && playerPositionInViewport.x < 1 &&
                playerPositionInViewport.y > 0 && playerPositionInViewport.y < 1)
            {
                Vector3 direction = (player.transform.position - frustum.transform.position).normalized;
                Ray ray = new Ray(frustum.transform.position, direction);
                RaycastHit hit;

                // Perform raycast to check if there is a line of sight
                if (Physics.Raycast(ray, out hit, frustum.farClipPlane, mask))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        return true; // Player detected
                    }
                }
            }

            return false; // Player not detected
        }
    }
}
