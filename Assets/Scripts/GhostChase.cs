using UnityEngine;

public class GhostChase : GhostBehaviour
{
    private void OnDisable()
    {
        this.ghost.scatter.Enable();
    }

    private void OnTriggerEnter(Collider other)
    {
        Node node = other.GetComponent<Node>();

        // Ne rien faire si le fantôme est effrayé
        if (node != null && enabled && !this.ghost.frightened.enabled)
        {
            Vector3 direction = Vector3.zero;
            float minDistance = float.MaxValue;

            // Trouve la direction disponible qui rapproche le plus du Pac-Man
            foreach (Vector3 availableDirection in node.availableDirections)
            {
                // Calcule la position potentielle dans cette direction
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, 0.0f, availableDirection.z);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

                // Si la distance dans cette direction est inférieure à la distance minimale actuelle,
                // cette direction devient la plus proche
                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            // Définit la nouvelle direction du fantôme
            this.ghost.movement.SetNextDirection(direction);
        }
    }
}
