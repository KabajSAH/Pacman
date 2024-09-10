using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform connection;

    private void OnTriggerEnter(Collider other)
    {
        // Assurez-vous que vous manipulez le GameObject principal
        Transform rootTransform = other.transform.root;

        // Arrête le mouvement pendant la téléportation pour éviter des comportements inattendus
        if (rootTransform.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.velocity = Vector3.zero; // Arrête le mouvement avant la téléportation
        }

        // Téléporte Pac-Man à la position de la connexion
        Vector3 position = rootTransform.position;
        position.x = this.connection.position.x;
        position.z = this.connection.position.z;
        rootTransform.position = position;

        // Restaure le mouvement après la téléportation
        if (rb != null)
        {
            rb.velocity = rb.velocity.normalized * rb.velocity.magnitude; // Rétablit la direction du mouvement
        }
    }
}
