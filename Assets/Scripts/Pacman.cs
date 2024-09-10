using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    public Movement movement { get; private set; }

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        HandleInput();
        HandleRotation();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.SetNextDirection(Vector3.forward); // Déplacement sur l'axe Z positif
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.SetNextDirection(Vector3.back); // Déplacement sur l'axe Z négatif
        }
        else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.SetNextDirection(Vector3.left); // Déplacement sur l'axe X négatif
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.SetNextDirection(Vector3.right); // Déplacement sur l'axe X positif
        }
    }

    private void HandleRotation()
    {
        // Applique la rotation uniquement si la direction change
        Vector3 direction = movement.direction;
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    public void ResetState()
    {
        //enabled = true;
        /*deathSequence.enabled = false;*/
        this.gameObject.SetActive(true);
        movement.ResetState();
    }
}
