using UnityEngine;
using UnityEngine.Tilemaps;

public class KnightMovement : MonoBehaviour
{
    public float moveSpeed = 3f;           // Speed at which the knight moves
    public Tilemap groundTilemap;          // Reference to the ground Tilemap (walkable)
    public Tilemap wallTilemap;            // Reference to the wall Tilemap (non-walkable)

    private Vector3Int currentGridPosition; // The knight's current grid position
    private bool isMoving = false;          // Flag to check if knight is in motion
    private Vector3 targetPosition;         // Target position for movement

    // Directions for movement
    private Vector3Int[] directions = new Vector3Int[] {
        Vector3Int.up,     // Move up
        Vector3Int.down,   // Move down
        Vector3Int.left,   // Move left
        Vector3Int.right   // Move right
    };
    private int currentDirectionIndex = 0;  // Current direction index in the array

    private void Start()
    {
        // Set initial grid position based on current knight position
        Vector3 worldPosition = transform.position;
        currentGridPosition = groundTilemap.WorldToCell(worldPosition); // Convert world position to grid
        SnapToGrid(); // Ensure the knight is perfectly aligned with the grid

        // Set initial movement direction
        MoveTo(directions[currentDirectionIndex]);  // Start by moving in the initial direction
    }

    private void Update()
    {
        // Move towards target position if moving
        if (isMoving)
        {
            MoveKnight();
        }
    }

    private void MoveTo(Vector3Int direction)
    {
        // Calculate the target grid position
        Vector3Int newGridPosition = currentGridPosition + direction;

        // Check if the new position is valid (walkable ground and not a wall)
        if (IsTileWalkable(newGridPosition))
        {
            // Set target position based on grid
            currentGridPosition = newGridPosition;
            targetPosition = groundTilemap.GetCellCenterWorld(currentGridPosition);
            isMoving = true; // Start movement
        }
        else
        {
            // If the knight encounters a wall, change direction
            ChangeDirection();
        }
    }

    private bool IsTileWalkable(Vector3Int gridPosition)
    {
        // Check if the tile at the target position is walkable (in the ground tilemap)
        TileBase groundTile = groundTilemap.GetTile(gridPosition);

        // Check if the tile at the target position is non-walkable (wall layer)
        TileBase wallTile = wallTilemap.GetTile(gridPosition);

        // The tile is walkable if it exists in the groundTilemap and there is no wall at that position
        return groundTile != null && wallTile == null;
    }

    private void MoveKnight()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // If the knight reaches the target position, stop moving and snap to grid
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            isMoving = false;
            SnapToGrid(); // Ensure perfect alignment with the grid

            // Continue moving in the current direction
            MoveTo(directions[currentDirectionIndex]);
        }
    }

    private void SnapToGrid()
    {
        // Snap the knight to the exact center of the current grid tile
        transform.position = groundTilemap.GetCellCenterWorld(currentGridPosition);
    }

    private void ChangeDirection()
    {
        // Increment the direction index to change direction
        currentDirectionIndex = (currentDirectionIndex + 1) % directions.Length;

        // Try moving in the new direction
        MoveTo(directions[currentDirectionIndex]);
    }

    public void Hurt()
    {
        // Reverse the current direction when the knight is hurt
        currentDirectionIndex = (currentDirectionIndex + 2) % directions.Length;
        // +2 because it's the opposite direction (e.g. Up <-> Down, Left <-> Right)

        // Move the knight in the new reversed direction
        MoveTo(directions[currentDirectionIndex]);
    }
}
