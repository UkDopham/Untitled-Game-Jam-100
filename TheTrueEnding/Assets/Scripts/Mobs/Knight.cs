using Assets.Scripts.Constants;
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

        Death();
    }
    private void Death()
    {
        this._animator.SetTrigger("death");
        PlayClip(this._deathClip);
        EndingManager.CurrentEndings.Add(Endings.death);
        this._endingUI.LoadEndingText($"The knight has died.");
    }
    public void Stop()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Ending ending = null;

        AudioDetector audioDetector = other.GetComponent<AudioDetector>();
        if (audioDetector != null)
        {
            audioDetector.PlayAudio();
            return;
        }

        Princess princess = other.GetComponent<Princess>();
        if (princess != null)
        {
            ending = princess.GetComponent<Ending>();
            if (ending.IsContitionsMet(this._items))
            {
                PlayClip(this._fightClip);
                princess.Death();
            }
            else
            {
                ending.StartEnding();
            }
            return;
        }

        Golem golem = other.GetComponent<Golem>();
        if (golem != null)
        {
            ending = golem.GetComponent<Ending>();
            if (this._items.Contains(ItemType.Sword))
            {
                PlayClip(this._fightClip);
                golem.Death();
            }
            else
            {
                PlayClip(golem.AttackClip);
                Death();
            }
        }

        Dragon dragon = other.GetComponent<Dragon>();
        if (dragon != null)
        {
            ending = dragon.GetComponent<Ending>();
            if (ending.IsContitionsMet(this._items))
            {
                ending.StartEnding();
                return;
            }

            if (this._items.Contains(ItemType.Sword) && this._items.Contains(ItemType.Shield))
            {
                PlayClip(this._fightClip);
                dragon.Death();
            }
            else
            {
                PlayClip(dragon.AttackClip);
                Death();
            }
            return;
        }

        Demon demon = other.GetComponent<Demon>();
        if (demon != null)
        {
            ending = demon.GetComponent<Ending>();
            if (this._items.Contains(ItemType.Sword))
            {
                PlayClip(this._fightClip);
                demon.Death();
            }
            else
            {
                PlayClip(this._fightClip);
                Death();
            }
        }

        Skeleton skeleton = other.GetComponent<Skeleton>();
        if (skeleton != null)
        {
            if (this._items.Contains(ItemType.Sword))
            {
                PlayClip(this._fightClip);
                skeleton.Death();
            }
            else
            {
                PlayClip(this._fightClip);
                Death();
            }
        }

        ending = other.GetComponent<Ending>();
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
