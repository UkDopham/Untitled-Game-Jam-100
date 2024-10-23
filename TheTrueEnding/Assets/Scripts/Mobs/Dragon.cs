using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    // [SerializeField]
    // private SpriteRenderer _emotionSpriteRenderer;
    [SerializeField]
    private const float speed = 2f;
    // [SerializeField]
    // private EmotionManager _emotionManager;
    // private List<ItemType> _items = new List<ItemType>();
    private DragonMovement _DragonMovement;
    private Animator _animator;

    // c quoi le plus propre? le bouger vers DragonMouvement avec variables publiques : laisser ici
    private float  _DragonLastMove = 0f; 
    [SerializeField]
    [Range(0f, 10f)]
    private float _idleTime = 3f;

    [SerializeField]
    [Range(0f, 10f)]
    private float _moveDuration = 4f;
    private bool _hasRoared = false;

    private void Awake()
    {
        this._DragonMovement = GetComponent<DragonMovement>();
        this._animator = GetComponent<Animator>();
    }

    private void Start(){
        this._DragonLastMove = Time.time;
    }
    // public void AddItem(ItemType type)
    // {
    //     if (this._items.Contains(type))
    //     {
    //         return;
    //     }

    //     this._items.Add(type);
    // }

    private void Update()
    {
        Patrol();
    }
    public void Patrol(){
        if (Time.time - this._DragonLastMove > this._idleTime + this._moveDuration) {
            this._DragonMovement.ChangeToOppositeDirection(); 
            this._DragonLastMove = Time.time;
            this._animator.SetTrigger("walk");
            this._hasRoared = false;
        }
        this._DragonMovement._isMoving = Time.time - this._DragonLastMove < this._moveDuration; 
        if (!_DragonMovement._isMoving){
            if (!this._hasRoared)
                this._animator.SetTrigger("scan");
            this._hasRoared = true;
        }
    }
    public void Hurt()
    {
        if(this._DragonMovement == null)
        {
            return;
        }

        // if (this._items.Contains(ItemType.Shield))
        // {
        //     this._animator.SetTrigger("shield");
        //     return;
        // }
        // this._animator.SetTrigger("hit");
        //Sprite emotionSprite = this._emotionManager.GetSpriteByEmotion(Emotion.Scared);
        //this._emotionSpriteRenderer.sprite = emotionSprite;
        // this._DragonMovement.ChangeToOppositeDirection();
    }
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     string name = other.gameObject.name;

    //     Skeleton skeleton = other.GetComponent<Skeleton>();

    //     if (skeleton == null)
    //     {
    //         return;
    //     }

    //     if (this._items.Contains(ItemType.Sword))
    //     {
    //         skeleton.Death();
    //     }
    // }
    
}
