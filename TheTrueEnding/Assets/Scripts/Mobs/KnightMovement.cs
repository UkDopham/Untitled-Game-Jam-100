using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;

public class KnightMovement : MonoBehaviour
{
    [SerializeField]
    private Pathfinding _pathfinding;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Tilemap _tilemap;
    private List<Point> _points;
    private Animator _animator;

    private List<Vector3Int> _path;
    private int _currentPathIndex = 0;
    private float _moveSpeed = 2f;
    private float idleTime =0f;

    void Start()
    {
        this._animator = GetComponent<Animator>();
        this._points = FindObjectsOfType<Point>().ToList();
        MoveToNearestPoint();
    }

    void Update()
    {
        if (_path != null && _path.Count > 0)
        {
            MoveAlongPath();
        }
        if (this.idleTime == 0)
            this.idleTime = Time.time;
        if (Time.time - this.idleTime > 1f)
            this._animator.SetTrigger("idle");
    }

    private void SetRunAnimation(Vector3 vectionDirection)
    {
        this.idleTime = 0f;
        if (vectionDirection.y < -0.8)
            this._animator.SetTrigger("run_bottom");
        else if (vectionDirection.y > 0.8)
            this._animator.SetTrigger("run_top");
        else if (vectionDirection.x > 0.8)
            this._animator.SetTrigger("run_right");
        else if (vectionDirection.x < -0.8)
            this._animator.SetTrigger("run_left");
        else
            Debug.Log($"No animation found. x={vectionDirection.x} y={vectionDirection.y}");
        Debug.Log($"Animation . x={vectionDirection.x} y={vectionDirection.y}");
    }
    public void MoveToNearestPoint()
    {
        if (_points.Count == 0)
        {
            return;
        }

        Vector3Int knightPosition = _tilemap.WorldToCell(this.transform.position);
        Point nearestPoint = null;
        float nearestDistance = float.MaxValue;

        foreach (Point point in _points)
        {
            Vector3Int pointTilePosition = _tilemap.WorldToCell(point.transform.position);
            float distance = Vector3Int.Distance(knightPosition, pointTilePosition);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestPoint = point;
            }
        }

        if (nearestPoint != null)
        {
            SetTarget(nearestPoint.transform);
        }
    }

    private void CalculatePathToTarget()
    {
        Vector3Int startTile = _tilemap.WorldToCell(this.transform.position);
        Vector3Int endTile = _tilemap.WorldToCell(this._target.position);

        _path = _pathfinding.FindPath(startTile, endTile);

        if (_path == null || _path.Count == 0)
        {
            Debug.Log("No path found to the target. Stopping movement.");
            _path = null;
            return;
        }

        _currentPathIndex = 0;
    }

    private void MoveAlongPath()
    {
        if (_currentPathIndex >= _path.Count)
        {
            return;
        }
        Vector3 tileCenterOffset = new Vector3(_tilemap.cellSize.x / 2, _tilemap.cellSize.y / 2, 0);
        Vector3 targetPosition = _tilemap.CellToWorld(_path[_currentPathIndex]) + tileCenterOffset;
        SetRunAnimation((targetPosition - gameObject.transform.position).normalized);

        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, _moveSpeed * Time.deltaTime);

        if (Vector3.Distance(this.transform.position, targetPosition) < 0.1f)
        {
            _currentPathIndex++;

            if (_currentPathIndex >= _path.Count)
            {
                Point visitedPoint = _points.Find(p => p.transform == _target);

                if (visitedPoint != null)
                {
                    _points.Remove(visitedPoint);
                    Destroy(visitedPoint.gameObject);
                }

                MoveToNearestPoint();
            }
        }
    }

    public void SetTarget(Transform newTarget)
    {
        _target = newTarget;
        CalculatePathToTarget();

        if (_path == null || _path.Count == 0)
        {
            Debug.Log("Unable to reach target. No valid path available.");
            _target = null;
        }
    }
}
