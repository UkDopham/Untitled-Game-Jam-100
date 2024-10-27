using UnityEngine;

public class Dragon : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _attackClip;
    [SerializeField]
    private AudioClip _deathClip;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    public AudioClip AttackClip
    {
        get
        {
            return _attackClip;
        }
    }

    private void Awake()
    {
        this._animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this._audioSource = GetComponent<AudioSource>();
        this._spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Death()
    {
        this._animator.SetTrigger("hit");
        // this._spriteRenderer.enabled = false;
        this._audioSource.clip = this._deathClip;
        this._audioSource.Play();
        Destroy(gameObject, 1f);
    }

}
