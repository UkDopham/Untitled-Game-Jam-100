using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System;

public class KnightMovement : MonoBehaviour
{
    [SerializeField]
    private Pathfinding _pathfinding;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Tilemap _tilemap;
    [SerializeField]
    private List<Point> _points = new List<Point>();
    private Animator _animator;

    private List<Vector3Int> _path;
    private int _currentPathIndex = 0;
    private float _moveSpeed = 2f;

    void Start()
    {
        this.MoveToNearestPoint();
        this._animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (this._path != null 
            && this._path.Count > 0)
        {
            try
            {
                MoveAlongPath();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        print($"hello {Guid.NewGuid()}");
    }

    private void MoveToNearestPoint()
    {
        if (this._points.Count == 0)
        {
            return;
        }

        Vector3Int knightPosition = this._tilemap.WorldToCell(this.transform.position);

        Point nearestPoint = null;
        float nearestDistance = float.MaxValue;

        foreach (Point point in this._points)
        {
            Vector3Int pointTilePosition = this._tilemap.WorldToCell(point.transform.position);
            float distance = Vector3Int.Distance(knightPosition, pointTilePosition);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestPoint = point;
            }
        }

        if (nearestPoint != null)
        {
            this.SetTarget(nearestPoint.transform);
        }
    }

    private void CalculatePathToTarget()
    {
        Vector3Int startTile = this._tilemap.WorldToCell(this.transform.position);
        Vector3Int endTile = this._tilemap.WorldToCell(this._target.position);

        this._path = this._pathfinding.FindPath(startTile, endTile);

        if (this._path == null || this._path.Count == 0)
        {
            Debug.Log("No path found, stopping movement.");
            this._path = null; // No valid path, knight should stop moving
            return;
        }

        this._currentPathIndex = 0;
    }

    private void MoveAlongPath()
    {
        if (this._currentPathIndex >= this._path.Count)
        {
            return;
        }

        Vector3 tileCenterOffset = new Vector3(this._tilemap.cellSize.x / 2, this._tilemap.cellSize.y / 2, 0);
        Vector3 targetPosition = this._tilemap.CellToWorld(this._path[this._currentPathIndex]) + tileCenterOffset;

        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, this._moveSpeed * Time.deltaTime);
        
        if (Vector3.Distance(this.transform.position, targetPosition) < 0.1f)
        {
            this._currentPathIndex++;

            if (this._currentPathIndex >= this._path.Count)
            {
                Point visitedPoint = this._points.Find(p => p.transform == this._target);

                if (visitedPoint != null)
                {
                    this._points.Remove(visitedPoint);
                    Destroy(visitedPoint.gameObject); // Optional: Destroy the point if needed
                }

                this.MoveToNearestPoint(); // Move to the next nearest point
            }
        }
    }

    public void SetTarget(Transform newTarget)
    {
        this._target = newTarget;
        this.CalculatePathToTarget();
    }
}
