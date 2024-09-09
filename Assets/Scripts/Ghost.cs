using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement movement {  get; private set; }
    public GhostHome home {  get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightened frightened { get; private set; }

    public GhostBehaviour initialBehaviour;

    public Transform target;

    public int points = 200;

    public void Awake()
    {
        this.movement = GetComponent<Movement> ();
        this.home = GetComponent<GhostHome>();
        this.scatter = GetComponent<GhostScatter>();
        this.chase = GetComponent<GhostChase>();
        this.frightened = GetComponent<GhostFrightened>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();

        if (this.home != this.initialBehaviour)
        {
            this.home.Disable();
        }

        if (this.initialBehaviour != null)
        {
            this.initialBehaviour.Enable();
        }
    }

    public void SetPosition(Vector3 position)
    {
        position.y = transform.position.y;
        transform.position = position;
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

}
