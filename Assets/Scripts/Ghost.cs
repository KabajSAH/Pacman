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

    private void Star()
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

}
