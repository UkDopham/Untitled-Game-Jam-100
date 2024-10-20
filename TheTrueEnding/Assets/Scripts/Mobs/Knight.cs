using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _emotionSpriteRenderer;
    [SerializeField]
    private const float speed = 2f;
    [SerializeField]
    private EmotionManager _emotionManager;
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

        if (this._items.Contains(ItemType.Shield))
        {
            return;
        }

        //Sprite emotionSprite = this._emotionManager.GetSpriteByEmotion(Emotion.Scared);
        //this._emotionSpriteRenderer.sprite = emotionSprite;
        this._knightMovement.ChangeToOppositeDirection();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        string name = other.gameObject.name;

        Skeleton skeleton = other.GetComponent<Skeleton>();

        if (skeleton == null)
        {
            return;
        }

        if (this._items.Contains(ItemType.Sword))
        {
            skeleton.Death();
        }
    }
}
