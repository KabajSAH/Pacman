
using UnityEngine;

public class GhostScatter : GhostBehaviour
{
    private void OnDisable()
    {
        ghost.chase.Enable();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger detected with: {other.name}");
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            int index = Random.Range(0, node.availableDirection.Count);

            if (node.availableDirection[index] == -this.ghost.movement.direction && node.availableDirection.Count > 1)
            {
                index++;

                if (index >= node.availableDirection.Count)
                {
                    index = 0;
                }
            }
            Debug.Log(node.availableDirection[index]);

            this.ghost.movement.SetDirection(node.availableDirection[index]);

        }
    }
}

