using UnityEngine;
using UnityEngine.Tilemaps;

public class KnightMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 3f;
    [SerializeField]
    private Tilemap _groundTilemap;
    [SerializeField]
    private Tilemap _wallTilemap;
    [SerializeField]
    [Range(0, 3)]
    private int _currentDirectionIndex = 3;

    private Vector3Int _currentGridPosition;
    private bool _isMoving = false;
    private Vector3 _targetPosition;

    private Vector3Int[] _directions = new Vector3Int[] {
        Vector3Int.up,
        Vector3Int.down,
        Vector3Int.left,
        Vector3Int.right
    };
    private void Start()
    {
        Vector3 worldPosition = transform.position;
        this._currentGridPosition = this._groundTilemap.WorldToCell(worldPosition);
        SnapToGrid();

        MoveTo(this._directions[this._currentDirectionIndex]);
    }
    private void Update()
    {
        if (this._isMoving)
        {
            MoveKnight();
        }
    }
    private void MoveTo(Vector3Int direction)
    {
        Vector3Int newGridPosition = this._currentGridPosition + direction;

        if (this.IsTileWalkable(newGridPosition))
        {
            this._currentGridPosition = newGridPosition;
            this._targetPosition = this._groundTilemap.GetCellCenterWorld(this._currentGridPosition);
            this._isMoving = true;
        }
        else
        {
            ChangeDirection();
        }
    }
    private bool IsTileWalkable(Vector3Int gridPosition)
    {
        TileBase groundTile = this._groundTilemap.GetTile(gridPosition);
        TileBase wallTile = this._wallTilemap.GetTile(gridPosition);
        return groundTile != null && wallTile == null;
    }
    private void MoveKnight()
    {
        transform.position = Vector3.MoveTowards(transform.position, this._targetPosition, this._moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, this._targetPosition) < 0.01f)
        {
            this._isMoving = false;
            SnapToGrid();

            MoveTo(this._directions[this._currentDirectionIndex]);
        }
    }
    private void SnapToGrid()
    {
        transform.position = this._groundTilemap.GetCellCenterWorld(this._currentGridPosition);
    }
    private void ChangeDirection()
    {
        this._currentDirectionIndex = (this._currentDirectionIndex + 1) % this._directions.Length;

        MoveTo(this._directions[this._currentDirectionIndex]);
    }
    public void ChangeToOppositeDirection()
    {
        this._currentDirectionIndex = GetOppositeDirectionIndex(this._currentDirectionIndex);

        MoveTo(this._directions[this._currentDirectionIndex]);
    }
    private int GetOppositeDirectionIndex(int directionIndex)
    {
        switch(directionIndex)
        {
            case 0:  
                // Up -> Down
                return 1;

            case 1:
                // Down -> Up
                return 0;

            case 2:
                // Left -> Right
                return 3;

            case 3:
                // Right -> Left
                return 2;
        }

        return 0;
    }
}
