using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    private const float speed = 2f;
    private List<ItemType> _items = new List<ItemType>();
    private KnightMovement _knightMovement;

    private void Awake()
    {
        this._knightMovement = GetComponent<KnightMovement>();
    }
    public void AddItem(ItemType type)
    {
        if (this._items.Contains(type))
        {
            return;
        }

        this._items.Add(type);
    }

    public void Hurt()
    {
        if(this._knightMovement == null)
        {
            return;
        }
        this._knightMovement.ChangeToOppositeDirection();
    }
}
