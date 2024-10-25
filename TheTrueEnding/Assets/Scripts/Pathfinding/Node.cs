using UnityEngine;

public class Node
{
    public Vector3Int _position;
    public bool _isWalkable;
    public Node _parent;
    public int _gCost;
    public int _hCost;

    public int FCost => _gCost + _hCost;

    public Node(Vector3Int position, bool isWalkable)
    {
        this._position = position;
        this._isWalkable = isWalkable;
    }
}

