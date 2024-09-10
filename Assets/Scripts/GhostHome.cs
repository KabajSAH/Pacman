using System.Collections;
using UnityEngine;

public class GhostHome : GhostBehaviour
{

    public Transform inside;
    public Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();

    }

    private void OnDisable()
    {
        if (this.gameObject.activeInHierarchy)
        {
            StartCoroutine(ExitTransition());
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            ghost.movement.SetNextDirection(-ghost.movement.direction);
        }

    }

    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetNextDirection(Vector3.forward, true);
        this.ghost.movement.rigidbody.isKinematic = true;
        this.ghost.movement.enabled = false;

        Vector3 position = this.transform.position;
        float duration = 0.5f;
        float elapsed = 0.0f;

        while ( elapsed < duration)
        {
            ghost.SetPosition(Vector3.Lerp(position, inside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;

        while (elapsed < duration)
        {
            ghost.SetPosition(Vector3.Lerp(inside.position, outside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        this.ghost.movement.SetNextDirection(new Vector3(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f, 0.0f), true);
        this.ghost.movement.rigidbody.isKinematic = false;
        this.ghost.movement.enabled = true;
        
    }

}
