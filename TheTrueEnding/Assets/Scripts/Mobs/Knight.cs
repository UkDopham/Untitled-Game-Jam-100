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
    [SerializeField]
    private EndingUI _endingUI;
    [SerializeField]
    private AudioClip _walkClip;
    [SerializeField]
    private AudioClip _fightClip;
    [SerializeField]
    private AudioClip _shieldClip;
    [SerializeField]
    private AudioClip _deathClip;
    [SerializeField]
    private AudioClip _itemClip;
    private List<ItemType> _items = new List<ItemType>();
    private KnightMovement _knightMovement;
    private Animator _animator;
    private AudioSource _audioSource;
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
        this._audioSource = GetComponent<AudioSource>();
    }
    public void AddItem(ItemType type)
    {
        if (this._items.Contains(type))
        {
            return;
        }

        this._items.Add(type);
    }
    private void PlayClip(AudioClip clip)
    {
        this._audioSource.Stop();
        this._audioSource.clip = clip;
        this._audioSource.Play();
    }
    public void Hit()
    {
        if (this._items.Contains(ItemType.Shield))
        {
            this._animator.SetTrigger("shield");
            PlayClip(this._shieldClip);
            return;
        }

        this._animator.SetTrigger("death");
        PlayClip(this._deathClip);
        EndingManager.CurrentEndings.Add(Assets.Scripts.Constants.Endings.death);
        Time.timeScale = 0f;
        this._endingUI.LoadEndingText($"DEATH");
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
    }
    private void OnTriggerEnter2D(Collider2D other)
    {        
        Ending ending = other.GetComponent<Ending>();
        if (ending != null)
        {
            ending.Interact(this._items);
            return;
        }

        Scroll scroll = other.GetComponent<Scroll>();
        if (scroll != null)
        {
            PlayClip(this._itemClip);
            scroll.DisplayScroll();
            return;
        }
        

        Skeleton skeleton = other.GetComponent<Skeleton>();
        if (skeleton != null)
        {
            if (this._items.Contains(ItemType.Sword))
            {
                PlayClip(this._fightClip);
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
        PlayClip(this._itemClip);
        int speed = potion.Speed;
        int durationInSeconds = potion.DurationInSeconds;
        float baseSpeed = this._movementSpeed;
        this._movementSpeed *= speed;

        yield return new WaitForSeconds(durationInSeconds);

        this._movementSpeed = baseSpeed;
        yield return null;
    }
}
