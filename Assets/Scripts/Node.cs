using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public List<Vector3> availableDirections { get; private set; }

    public void Start()
    {
        this.availableDirections = new List<Vector3>();

        // V�rification des directions dans l'espace 3D
        CheckAvailableDirections(Vector3.forward);  // Z positif
        CheckAvailableDirections(Vector3.back);     // Z n�gatif
        CheckAvailableDirections(Vector3.left);     // X n�gatif
        CheckAvailableDirections(Vector3.right);    // X positif
    }

    private void CheckAvailableDirections(Vector3 direction)
    {
        // Utilisation de BoxCast en 3D pour d�tecter les obstacles
        RaycastHit hit;
        bool isHit = Physics.BoxCast(this.transform.position, Vector3.one * 0.5f, direction, out hit, Quaternion.identity, 1.0f, this.obstacleLayer);

        if (!isHit)
        {
            this.availableDirections.Add(direction);
        }
    }
}
