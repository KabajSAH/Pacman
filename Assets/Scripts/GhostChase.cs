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

           
            foreach (Vector3 availableDirection in node.availableDirections)
            {
                
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, 0.0f, availableDirection.z);
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                
                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            
            ghost.movement.SetNextDirection(direction);
        }
    }
}
