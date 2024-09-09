using UnityEngine;

public class GhostFrightened : GhostBehaviour
{
    public MeshRenderer body;
    public MeshRenderer eyes;
    public MeshRenderer blue;
    public MeshRenderer white;

    public bool eaten {  get; private set; }

    public override void Enable(float duration)
    {
        base.Enable(duration);

        this.body.enabled = false;
        this.eyes.enabled = false;
        this.white.enabled = false;
        this.blue.enabled = true;

        Invoke(nameof(Flash), duration / 2f);
    }

    public override void Disable()
    {
        base.Disable();

        this.body.enabled = true;
        this.eyes.enabled = true;
        this.white.enabled = false;
        this.blue.enabled = false;
    }

    private void Eaten()
    {
        this.eaten = true;

        Vector3 position = this.ghost.home.inside.position;
        position.y = this.ghost.transform.position.y;
        this.ghost.transform.position = position;

        this.ghost.home.Enable(this.duration);

        this.body.enabled = false;
        this.eyes.enabled = true;
        this.white.enabled = false;
        this.blue.enabled = false;
    }
    
    private void Flash()
    {
        if (!this.eaten)
        {
            this.white.enabled = true;
            this.blue.enabled = false;
        }
    }

    private void onEnable() 
    {
        this.ghost.movement.speedMultiplier = 0.5f;
        this.eaten = false;
    }

    private void OnDisable() 
    {
        this.ghost.movement.speedMultiplier = 1.0f;
        this.eaten = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.enabled)
            {
                Eaten();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled)
        {
            Vector3 direction = Vector3.zero;
            float maxDistance = float.MinValue;

            foreach (Vector3 availableDirection in node.availableDirection)
            {
                Vector3 newPosition = transform.position + availableDirection;
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction);
        }
    }
}
