using System.Collections;
using UnityEngine;
public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemType _itemType;

    private Vector3 _startPosition;
    private bool _isDragging = false;
    private bool _shieldGiven = false;
    private Collider2D _knightCollider;
    private Knight _knight;

    private void Start()
    {
        this._startPosition = transform.position;

        this._knight = FindAnyObjectByType<Knight>();
        this._knightCollider = this._knight.GetComponent<Collider2D>();
    }
    private void Update()
    {
        if (this._isDragging 
            && !this._shieldGiven)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        }
    }
    private void OnMouseDown()
    {
        this._isDragging = true;
    }
    private void OnMouseUp()
    {
        this._isDragging = false;

        if (this._knightCollider != null 
            && this._shieldGiven == false)
        {
            if (IsItemDroppedOnKnight())
            {
                GiveItemToKnight();
            }
            else
            {
                ResetItemPosition();
            }
        }
    }
    private bool IsItemDroppedOnKnight()
    {
        return this._knightCollider.bounds.Contains(transform.position);
    }
    private void GiveItemToKnight()
    {
        Knight knight = this._knightCollider.GetComponent<Knight>();
        if (knight != null)
        {
            if (this._itemType == ItemType.Potion)
            {
                Potion potion = GetComponentInChildren<Potion>();
                if (potion != null)
                {
                    this._knight.UsePotion(potion);
                }
            }

            knight.AddItem(this._itemType);
            this._shieldGiven = true;
            Destroy(gameObject);
        }
    }

    private void ResetItemPosition()
    {
        transform.position = this._startPosition;
    }
}
