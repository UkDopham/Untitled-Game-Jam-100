using System.Collections.Generic;
using UnityEngine;

public class Princess : MonoBehaviour
{
    // [SerializeField]
    // private SpriteRenderer _emotionSpriteRenderer;
    [SerializeField]
    private const float speed = 2f;
    // [SerializeField]
    // private EmotionManager _emotionManager;
    // private List<ItemType> _items = new List<ItemType>();
    private PrincessMovement _PrincessMovement;
    private Animator _animator;

    // c quoi le plus propre? le bouger vers PrincessMouvement avec variables publiques : laisser ici
    private float  _princessLastMove = 0f; 
    [SerializeField]
    [Range(0f, 3f)]
    private float _idleTime = 2f;

    [SerializeField]
    [Range(0f, 3f)]
    private float _moveDuration = 1f;

    private void Awake()
    {
        this._PrincessMovement = GetComponent<PrincessMovement>();
        this._animator = GetComponent<Animator>();
    }

    private void Start(){
        this._princessLastMove = Time.time;
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
        Stroll();
    }
    public void Stroll(){
        // je compte sur le pro du code pour arranger tout ca
        if (Time.time - this._princessLastMove > this._idleTime + this._moveDuration) {
            this._PrincessMovement.ChangeToOppositeDirection(); // change de direction et marche quand elle se fait chier
            this._princessLastMove = Time.time;
            this._animator.SetTrigger("walk");
        }
        this._PrincessMovement._isMoving = Time.time - this._princessLastMove < this._moveDuration; // marche tant quelle a pas marre
        if (!_PrincessMovement._isMoving)
            this._animator.SetTrigger("stop");
    }
    public void Hurt()
    {
        if(this._PrincessMovement == null)
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
        // this._PrincessMovement.ChangeToOppositeDirection();
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
