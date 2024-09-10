using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform connection;

    private void OnTriggerEnter(Collider other)
    {
        
        // Arrête le mouvement pendant la téléportation pour éviter des comportements inattendus
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // Arrête le mouvement avant la téléportation
        }

        // Téléporte Pac-Man à la position de la connexion
        Vector3 position = other.transform.position;
        position.x = this.connection.position.x;
        position.z = this.connection.position.z;
        other.transform.position = position;

        // Restaure le mouvement après la téléportation
        if (rb != null)
        {
            rb.velocity = rb.velocity.normalized * rb.velocity.magnitude; // Rétablit la direction du mouvement
        }
        
    }
}
