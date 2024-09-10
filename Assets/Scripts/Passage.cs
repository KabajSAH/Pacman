using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform connection;

    private void OnTriggerEnter(Collider other)
    {
        
        // Arr�te le mouvement pendant la t�l�portation pour �viter des comportements inattendus
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // Arr�te le mouvement avant la t�l�portation
        }

        // T�l�porte Pac-Man � la position de la connexion
        Vector3 position = other.transform.position;
        position.x = this.connection.position.x;
        position.z = this.connection.position.z;
        other.transform.position = position;

        // Restaure le mouvement apr�s la t�l�portation
        if (rb != null)
        {
            rb.velocity = rb.velocity.normalized * rb.velocity.magnitude; // R�tablit la direction du mouvement
        }
        
    }
}
