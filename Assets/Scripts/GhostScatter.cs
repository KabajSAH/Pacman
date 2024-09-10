using UnityEngine;

public class GhostScatter : GhostBehaviour
{

    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }
    private void OnTriggerEnter(Collider other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            Vector3 selectedDirection = Vector3.zero;
            bool directionFound = false;

            // Essaye de trouver une direction non bloquée
            for (int i = 0; i < node.availableDirections.Count; i++)
            {
                int index = Random.Range(0, node.availableDirections.Count);

                // Vérifie que la direction n'est pas opposée à la direction actuelle (pour éviter le demi-tour)
                // et qu'elle n'est pas bloquée par un obstacle
                if (node.availableDirections[index] != -this.ghost.movement.direction && !IsDirectionBlocked(node.availableDirections[index]))
                {
                    selectedDirection = node.availableDirections[index];
                    directionFound = true;
                    break;
                }
            }

            // Si aucune direction non bloquée n'est trouvée, prend une direction disponible aléatoire
            if (!directionFound && node.availableDirections.Count > 0)
            {
                selectedDirection = node.availableDirections[Random.Range(0, node.availableDirections.Count)];
            }

            // Défini la direction suivante du fantôme
            this.ghost.movement.SetNextDirection(selectedDirection);
        }
    }

    private bool IsDirectionBlocked(Vector3 direction)
    {
        // Effectue un BoxCast pour vérifier s'il y a un obstacle dans la direction donnée
        RaycastHit hit;
        float distance = 3.0f;  // Ajuste la distance en fonction de la taille des éléments du labyrinthe

        bool isBlocked = Physics.BoxCast(this.ghost.transform.position, Vector3.one * 0.5f, direction, out hit, Quaternion.identity, distance, this.ghost.movement.obstacleLayer);

        return isBlocked;
    }
}
