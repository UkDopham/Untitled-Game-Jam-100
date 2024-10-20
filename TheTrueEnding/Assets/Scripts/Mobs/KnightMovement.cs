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

    private Vector3Int _currentGridPosition;
    private bool _isMoving = false;
    private Vector3 _targetPosition;

    private Vector3Int[] _directions = new Vector3Int[] {
        Vector3Int.up,
        Vector3Int.down,
        Vector3Int.left,
        Vector3Int.right
    };
    private int _currentDirectionIndex = 0;
    private void Start()
    {
        Vector3 worldPosition = transform.position;
        this._currentGridPosition = this._groundTilemap.WorldToCell(worldPosition);
        this.SnapToGrid();

        this.MoveTo(this._directions[this._currentDirectionIndex]);
    }
    private void Update()
    {
        if (this._isMoving)
        {
            this.MoveKnight();
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
            this.ChangeDirection();
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
            this.SnapToGrid();

            this.MoveTo(this._directions[this._currentDirectionIndex]);
        }
    }
    private void SnapToGrid()
    {
        transform.position = this._groundTilemap.GetCellCenterWorld(this._currentGridPosition);
    }
    private void ChangeDirection()
    {
        this._currentDirectionIndex = (this._currentDirectionIndex + 1) % this._directions.Length;

        this.MoveTo(this._directions[this._currentDirectionIndex]);
    }
    public void ChangeToOppositeDirection()
    {
        this._currentDirectionIndex = (this._currentDirectionIndex + 2) % this._directions.Length;

        this.MoveTo(this._directions[this._currentDirectionIndex]);
    }
}
