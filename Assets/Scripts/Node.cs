using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector3> availableDirection { get; private set; }
    public LayerMask obstacleLayer;

    private void Start()
    {

        availableDirection = new List<Vector3>();

        CheckAvailableDirection(Vector3.forward);  // Z+ axis
        CheckAvailableDirection(Vector3.back);     // Z- axis
        CheckAvailableDirection(Vector3.left);     // X- axis
        CheckAvailableDirection(Vector3.right);    // X+ axis
    }

    private void CheckAvailableDirection(Vector3 direction)
    {
        RaycastHit hit;

        bool hasHit = Physics.BoxCast(transform.position, Vector3.one * 0.5f, direction, out hit, Quaternion.identity, 1.5f, obstacleLayer);


        if (!hasHit)
        {

            availableDirection.Add(direction);
        }
    }

}
