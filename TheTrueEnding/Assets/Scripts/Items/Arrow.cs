using UnityEngine;
using UnityEngine.Tilemaps;

public class Arrow : MonoBehaviour
{
    public float speed = 5f;           // Speed of the arrow
    private Vector3 _direction;        // The direction the arrow will travel in
    public int damage = 1;             // Damage dealt by the arrow

    // This function is called to set the initial direction for the arrow to travel in
    public void SetTarget(Vector3 targetPosition)
    {
        // Set the direction once when the arrow is fired, so it moves in a straight line
        _direction = (targetPosition - transform.position).normalized;
    }

    private void Update()
    {
        // Move the arrow in the pre-set direction
        transform.Translate(_direction * speed * Time.deltaTime, Space.World);
    }

    // Handle collisions with walls and the knight
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the arrow hits the knight
        if (other.GetComponent<KnightMovement>() != null)
        {
            KnightMovement knightMovement = other.GetComponent<KnightMovement>();
            if (knightMovement != null)
            {
                knightMovement.Hurt();
            }

            // Destroy the arrow after it hits the knight
            Destroy(gameObject);
        }
        // Check if the arrow hits a wall
        else if (other.GetComponent<TilemapCollider2D>() != null)
        {
            Destroy(gameObject);
        }
    }
}
