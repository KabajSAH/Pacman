using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform connection;

    private void OnTriggerEnter(Collider other)
    {
        // Assurez-vous que vous manipulez le GameObject principal
        Transform rootTransform = other.transform.root;

        // Arr�te le mouvement pendant la t�l�portation pour �viter des comportements inattendus
        if (rootTransform.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.velocity = Vector3.zero; // Arr�te le mouvement avant la t�l�portation
        }

        // T�l�porte Pac-Man � la position de la connexion
        Vector3 position = rootTransform.position;
        position.x = this.connection.position.x;
        position.z = this.connection.position.z;
        rootTransform.position = position;

        // Restaure le mouvement apr�s la t�l�portation
        if (rb != null)
        {
            rb.velocity = rb.velocity.normalized * rb.velocity.magnitude; // R�tablit la direction du mouvement
        }
    }
}
