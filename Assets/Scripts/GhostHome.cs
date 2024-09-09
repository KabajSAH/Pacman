using UnityEngine;
using System.Collections;

public class GhostHome : GhostBehaviour
{
    public Transform inside;
    public Transform outside;

    private void OnDisable()
    {
        if (this.gameObject.activeSelf){
            StartCoroutine(ExitTransition());
        }
    }

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetDirection(Vector3.forward, true);
        this.ghost.movement.GetComponent<Rigidbody>().isKinematic = true;
        this.ghost.movement.enabled = false;

        Vector3 position = this.transform.position;
        float duration = 0.8f;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.inside.position, elapsed / duration);
            newPosition.z = position.z;  
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }


        elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(this.inside.position, this.outside.position, elapsed / duration);
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }


        Vector3 vector = new Vector3(Random.value < 0.5f ? -1f : 1f, 0f, 0f);

        this.ghost.movement.SetDirection(vector, true);
        this.ghost.movement.GetComponent<Rigidbody>().isKinematic = false;
        this.ghost.movement.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Debug.Log("Z = " + this.ghost.movement.direction);

            this.ghost.movement.SetDirection(-this.ghost.movement.direction);
            Debug.Log("Z = " + -this.ghost.movement.direction);


        }
    }

}
