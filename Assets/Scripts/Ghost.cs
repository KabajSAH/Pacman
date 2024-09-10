using UnityEngine;

[DefaultExecutionOrder(-10)]
[RequireComponent(typeof(Movement))]
public class Ghost : MonoBehaviour
{
    public Movement movement { get; private set; }

    public GhostHome home { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightened frightened { get; private set; }

    public GhostBehaviour initialBehaviour;

    public Transform target;

    public int points = 200;

    private Vector3 lastPosition;
    private float stuckTimer = 0f;
    private const float stuckThreshold = 1f; // Temps en secondes avant de changer de direction

    public void Awake()
    {
        movement = GetComponent<Movement>();
        home = GetComponent<GhostHome>();
        scatter = GetComponent<GhostScatter>();
        chase = GetComponent<GhostChase>();
        frightened = GetComponent<GhostFrightened>();
    }

    private void Start()
    {
        ResetState();
        lastPosition = transform.position;
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        movement.ResetState();
        frightened.Disable();
        chase.Disable();
        scatter.Enable();

        if (home != initialBehaviour)
        {
            home.Disable();
        }

        if (initialBehaviour != null)
        {
            initialBehaviour.Enable();
        }

        lastPosition = transform.position;
        stuckTimer = 0f;
    }

    private void Update()
    {
        CheckIfStuck();
    }

    private void CheckIfStuck()
    {
        // Vérifie si le fantôme a bougé depuis la dernière vérification
        if (Vector3.Distance(transform.position, lastPosition) < 0.1f)
        {
            stuckTimer += Time.deltaTime;

            // Si le fantôme est bloqué depuis plus de 3 secondes, change la direction
            if (stuckTimer >= stuckThreshold)
            {
                ChangeDirection();
                stuckTimer = 0f; // Réinitialiser le timer après avoir changé de direction
            }
        }
        else
        {
            // Réinitialise le timer si le fantôme a bougé
            stuckTimer = 0f;
            lastPosition = transform.position;
        }
    }

    private void ChangeDirection()
    {
        Node currentNode = FindClosestNode();
        if (currentNode != null)
        {
            // Essaye de trouver une nouvelle direction non bloquée
            foreach (Vector3 direction in currentNode.availableDirections)
            {
                if (direction != -movement.direction && !IsDirectionBlocked(direction))
                {
                    movement.SetNextDirection(direction);
                    break;
                }
            }
        }
    }

    private Node FindClosestNode()
    {
        // Trouve le Node le plus proche de la position actuelle du fantôme
        Node[] nodes = FindObjectsOfType<Node>();
        Node closestNode = null;
        float minDistance = float.MaxValue;

        foreach (Node node in nodes)
        {
            float distance = Vector3.Distance(transform.position, node.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestNode = node;
            }
        }

        return closestNode;
    }

    private bool IsDirectionBlocked(Vector3 direction)
    {
        // Vérifie si la direction est bloquée par un obstacle
        RaycastHit hit;
        float distance = 1.0f;  // Ajuste la distance selon la taille des éléments du labyrinthe

        return Physics.BoxCast(transform.position, Vector3.one * 0.5f, direction, out hit, Quaternion.identity, distance, movement.obstacleLayer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.frightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }

    public void SetPosition(Vector3 position)
    {
        // Keep the z-position the same since it determines draw depth
        position.y = transform.position.y;
        transform.position = position;
    }

}
