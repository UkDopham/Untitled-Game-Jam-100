using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _emotionSpriteRenderer;
    [SerializeField]
    private float _movementSpeed = 3f;
    [SerializeField]
    private EmotionManager _emotionManager;
    private List<ItemType> _items = new List<ItemType>();
    private KnightMovement _knightMovement;
    private Animator _animator;

    public float MovementSpeed
    {
        get
        {
            return _movementSpeed;
        }
        set
        {
            this._movementSpeed = value;
        }
    }
    private void Awake()
    {
        this._knightMovement = GetComponent<KnightMovement>();
        this._animator = GetComponent<Animator>();
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
            this._animator.SetTrigger("shield");
            return;
        }
        this._animator.SetTrigger("hit");
        //Sprite emotionSprite = this._emotionManager.GetSpriteByEmotion(Emotion.Scared);
        //this._emotionSpriteRenderer.sprite = emotionSprite;
        //this._knightMovement.ChangeToOppositeDirection();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {        
        Door door = other.GetComponent<Door>();
        if (door != null)
        {
            if (!door.isOpen)
            {
                //this._knightMovement.ChangeToOppositeDirection();
            }
            return;
        }

        Ending ending = other.GetComponent<Ending>();
        if (ending != null)
        {
            ending.Interact(this._items);
            return;
        }

        Scroll scroll = other.GetComponent<Scroll>();
        if (scroll != null)
        {
            scroll.DisplayScroll();
            return;
        }
        

        Skeleton skeleton = other.GetComponent<Skeleton>();
        if (skeleton != null)
        {
            if (this._items.Contains(ItemType.Sword))
            {
                skeleton.Death();
            }
            return;
        }            
    }

    public void UsePotion(Potion potion)
    {
        StartCoroutine(StartPotion(potion));
    }

    IEnumerator StartPotion(Potion potion)
    {
        int speed = potion.Speed;
        int durationInSeconds = potion.DurationInSeconds;
        float baseSpeed = this._movementSpeed;
        this._movementSpeed *= speed;

        yield return new WaitForSeconds(durationInSeconds);

        this._movementSpeed = baseSpeed;
        yield return null;
    }
}
