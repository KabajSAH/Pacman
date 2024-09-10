using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;
    public Vector3 initialDirection;
    public Vector3 direction { get; private set; }
    public Vector3 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }

    public LayerMask obstacleLayer;

    public new Rigidbody rigidbody { get; private set; }

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        this.startingPosition = this.transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.speedMultiplier = 1.0f;
        this.direction = this.initialDirection;
        this.nextDirection = Vector3.zero;
        this.transform.position = this.startingPosition;
        this.rigidbody.isKinematic = false;
        this.enabled = true;
    }

    private void Update()
    {
        HandleDirectionChange();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HandleDirectionChange()
    {
        if (this.nextDirection != Vector3.zero && !Occupied(this.nextDirection))
        {
            this.direction = this.nextDirection;
            this.nextDirection = Vector3.zero;
        }
    }

    private void Move()
    {
        Vector3 position = this.rigidbody.position;
        Vector3 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
        this.rigidbody.MovePosition(position + translation);
    }

    public void SetNextDirection(Vector3 newDirection, bool forced = false)
    {
        if (forced || !Occupied(newDirection))
        {
            this.direction = newDirection;
            this.nextDirection = Vector3.zero;
        }
        this.nextDirection = newDirection;
    }

    public bool Occupied(Vector3 direction)
    {
        RaycastHit hit;
        bool isOccupied = Physics.BoxCast(this.transform.position, Vector3.one * 0.5f, direction, out hit, Quaternion.identity, 0.5f, this.obstacleLayer);
        return isOccupied;
    }

}
