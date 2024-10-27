using System.Collections;
using UnityEngine;
public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemType _itemType;

    private ItemUI _itemUI;
    private Vector3 _startPosition;
    private bool _isDragging = false;
    private bool _shieldGiven = false;
    private Collider2D _knightCollider;
    private Knight _knight;
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private ScrollUI _scrollUI;

    public Sprite Sprite
    {
        get
        {
            return this._spriteRenderer.sprite;
        }
    }
    private void Start()
    {
        this._startPosition = transform.position;

        this._knight = FindAnyObjectByType<Knight>();
        this._knightCollider = this._knight.GetComponent<Collider2D>();
        this._audioSource = GetComponent<AudioSource>();
        this._spriteRenderer = GetComponent<SpriteRenderer>();
        this._itemUI = FindAnyObjectByType<ItemUI>();
        this._scrollUI = FindAnyObjectByType<ScrollUI>();
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

            if(this._itemType == ItemType.Scroll)
            {
                this._scrollUI.DisplayScroll("Beware the princess, for she harbors a dark secret. \n" +
                    "The one you seek to save is no maiden in distress, but a demon, poised to unleash chaos upon the realm. \n" +
                    "Only the dragon, feared by many, holds the key to her defeat. Do not free her.");
            }
            this._itemUI.AddItem(this);
            this._audioSource.Play();
            knight.AddItem(this._itemType);
            this._spriteRenderer.enabled = false;
            this._shieldGiven = true;
            Destroy(gameObject, 0.5f);
        }
    }

    private void ResetItemPosition()
    {
        transform.position = this._startPosition;
    }
}
