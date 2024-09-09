using UnityEngine;
using System.Collections;

public class GhostHome : GhostBehaviour
{
    public Transform inside;

    public Transform outside;

    private void OnDisable()
    {
        StartCoroutine(ExitTransition());
    }

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetDirection(Vector3.forward, true);
        this.ghost.movement.rigidbody.isKinamatic = true;  
        this.ghost.movement.enabled = false;


        Vector3 position = this.transform.position;

        float duration = 0.5f;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector4 newPosition = Vector3.Lerp(position, this.inside.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector4 newPosition = Vector3.Lerp(this.inside.position, this.outside.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        ghost.movement.SetDirection(new Vector3(Random.value < 0.5f ? -1f : 1f, 0f, 0f), true);
        this.ghost.movement.SetDirection(Vector3.forward, true);
        this.ghost.movement.rigidbody.isKenamatic = false;
        this.ghost.mivement.enabled = true;

    }
}
