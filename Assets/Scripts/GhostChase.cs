using UnityEngine;

public class GhostChase : GhostBehaviour
{
    private void OnDisable()
    {
        ghost.scatter.Enable();
    }

    private void OnTriggerEnter(Collider other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled && !ghost.frightened.enabled)
        {
            Vector3 direction = Vector3.zero;
            float minDistance = float.MaxValue;

            foreach (Vector3 availableDirection in node.availableDirection)
            {
                Vector3 newPosition = transform.position + availableDirection;
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction);
        }
    }
}
