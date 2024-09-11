using UnityEngine;

public class GhostFrightened : GhostBehaviour
{
    public Renderer bodyRenderer;
    public Color frightenedColor = Color.blue;
    public Color flashingColor = Color.white;

    private Color originalColor;
    private bool eaten;

    public override void Enable(float duration)
    {
        base.Enable(duration);

        originalColor = bodyRenderer.material.color;  // Sauvegarde la couleur originale
        bodyRenderer.material.color = frightenedColor;  // Change la couleur pour indiquer l'état "frightened"

        Invoke(nameof(Flash), duration / 2f);
    }

    public override void Disable()
    {
        base.Disable();

        bodyRenderer.material.color = originalColor;  // Rétablit la couleur originale
        eaten = false;
    }

    private void Eaten()
    {
        eaten = true;
        ghost.SetPosition(ghost.home.inside.position);
        ghost.home.Enable(duration);

        bodyRenderer.material.color = originalColor;  // Rétablit la couleur originale après avoir été mangé
    }

    private void Flash()
    {
        if (!eaten)
        {
            bodyRenderer.material.color = flashingColor;  // Change la couleur pour le "flashing"
        }
    }

    private void OnEnable()
    {
        ghost.movement.speedMultiplier = 0.5f;
        eaten = false;
    }

    private void OnDisable()
    {
        ghost.movement.speedMultiplier = 1f;
        eaten = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled)
        {
            Vector3 direction = Vector3.zero;
            float maxDistance = float.MinValue;

            // Trouve la direction disponible qui éloigne le plus du Pac-Man
            foreach (Vector3 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, 0.0f, availableDirection.z); ;
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            ghost.movement.SetNextDirection(direction);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (enabled)
            {
                Eaten();
            }
        }
    }
}
 