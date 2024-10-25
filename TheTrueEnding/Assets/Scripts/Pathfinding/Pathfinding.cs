using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{
    [SerializeField]
    private Tilemap _tilemap;
    private List<Door> _doors;

    private void Awake()
    {
        this._doors = FindObjectsOfType<Door>().ToList();
    }

    public List<Vector3Int> FindPath(Vector3Int start, Vector3Int end)
    {
        if (!IsTileWalkable(start) || !IsTileWalkable(end))
        {
            Debug.Log("Start or end position is not walkable.");
            return null;
        }

        Queue<Node> queue = new Queue<Node>();
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();

        queue.Enqueue(new Node(start, true));
        visited.Add(start);

        while (queue.Count > 0)
        {
            Node currentNode = queue.Dequeue();

            if (currentNode._position == end)
            {
                return RetracePath(start, end, cameFrom);
            }

            foreach (Vector3Int direction in new Vector3Int[] { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right })
            {
                Vector3Int neighborPos = currentNode._position + direction;

                if (!visited.Contains(neighborPos) && IsTileWalkable(neighborPos))
                {
                    queue.Enqueue(new Node(neighborPos, true));
                    visited.Add(neighborPos);
                    cameFrom[neighborPos] = currentNode._position;
                }
            }
        }

        Debug.Log("No path found.");
        return null;
    }

    private List<Vector3Int> RetracePath(Vector3Int start, Vector3Int end, Dictionary<Vector3Int, Vector3Int> cameFrom)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        Vector3Int current = end;

        while (current != start)
        {
            path.Add(current);
            current = cameFrom[current];
        }
        path.Reverse();
        return path;
    }

    private bool IsTileWalkable(Vector3Int tilePosition)
    {
        TileBase tile = this._tilemap.GetTile(tilePosition);
        if (tile == null)
        {
            return false;
        }
        foreach (Door door in this._doors)
        {
            if (door.IsOnSameTile(tilePosition) && !door.isOpen)
            {
                return false;
            }
        }
        return true;
    }
}
