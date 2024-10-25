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
        Node startNode = new Node(start, this.IsTileWalkable(start));
        Node endNode = new Node(end, this.IsTileWalkable(end));

        if (!startNode._isWalkable || !endNode._isWalkable)
        {
            Debug.Log("Either the start or end node is not walkable.");
            return null;
        }

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        openList.Add(startNode);
        int j = 0;
        while (openList.Count > 0)
        {
            print($"Hello {j}");
            Node currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].FCost < currentNode.FCost || (openList[i].FCost == currentNode.FCost && openList[i]._hCost < currentNode._hCost))
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode._position == endNode._position)
            {
                return this.RetracePath(startNode, currentNode);
            }

            foreach (Node neighbor in this.GetNeighbors(currentNode))
            {
                if (!neighbor._isWalkable || closedList.Contains(neighbor))
                    continue;

                int newGCost = currentNode._gCost + this.GetDistance(currentNode, neighbor);
                if (newGCost < neighbor._gCost || !openList.Contains(neighbor))
                {
                    neighbor._gCost = newGCost;
                    neighbor._hCost = this.GetDistance(neighbor, endNode);
                    neighbor._parent = currentNode;

                    if (!openList.Contains(neighbor))
                        openList.Add(neighbor);
                }
            }
            j++;
        }

        Debug.Log("No path found.");
        return null; // No path found, return null to indicate failure.
    }
    private List<Vector3Int> RetracePath(Node startNode, Node endNode)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode._position);
            currentNode = currentNode._parent;
        }

        path.Reverse();
        return path;
    }
    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();
        Vector3Int[] directions = { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right };

        foreach (Vector3Int direction in directions)
        {
            Vector3Int neighborPos = node._position + direction;
            neighbors.Add(new Node(neighborPos, this.IsTileWalkable(neighborPos)));
        }

        return neighbors;
    }
    private int GetDistance(Node a, Node b)
    {
        return Mathf.Abs(a._position.x - b._position.x) + Mathf.Abs(a._position.y - b._position.y);
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
