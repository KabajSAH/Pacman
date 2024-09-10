using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public readonly  List<Vector3> availableDirections = new();

    public void Start()
    {
        availableDirections.Clear();

        // Vérification des directions dans l'espace 3D
        CheckAvailableDirections(Vector3.forward);  // Z positif
        CheckAvailableDirections(Vector3.back);     // Z négatif
        CheckAvailableDirections(Vector3.left);     // X négatif
        CheckAvailableDirections(Vector3.right);    // X positif
    }

    private void CheckAvailableDirections(Vector3 direction)
    {
        // Utilisation de BoxCast en 3D pour détecter les obstacles
        RaycastHit hit;
        bool isHit = Physics.BoxCast(this.transform.position, Vector3.one * 0.5f, direction, out hit, Quaternion.identity, 1.2f, this.obstacleLayer);

        if (!isHit)
        {
            this.availableDirections.Add(direction);
        }
    }
}
