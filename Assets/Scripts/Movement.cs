using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public float speed = 8f;
    public float speedMultiplier = 1f;
    public Vector3 initialDirection;
    public LayerMask obstacleLayer;

    public Rigidbody rb { get; private set; }
    public Vector3 direction { get; private set; }
    public Vector3 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = this.transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        speedMultiplier = 1f;
        direction = initialDirection;
        nextDirection = Vector3.zero;
        transform.position = startingPosition;
        rb.isKinematic = false;
        enabled = true;
    }

    private void Update()
    {
        if (nextDirection != Vector3.zero)
        {
            SetDirection(nextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector3 position = rb.position;
        Vector3 translation = speed * speedMultiplier * Time.fixedDeltaTime * direction;

        rb.MovePosition(position + translation);
    }

    public void SetDirection(Vector3 direction, bool forced = false)
    {

        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            nextDirection = Vector3.zero;
        }
        else
        {
            nextDirection = direction;
        }
    }

    public bool Occupied(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.BoxCast(transform.position, Vector3.one * 0.75f, direction, out hit, Quaternion.identity, 1.5f, obstacleLayer))
        {
            return hit.collider != null;
        }
        return false;
    }


}