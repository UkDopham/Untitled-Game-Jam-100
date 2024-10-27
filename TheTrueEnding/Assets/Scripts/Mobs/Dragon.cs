using UnityEngine;

public class Dragon : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _attackClip;
    [SerializeField]
    private AudioClip _deathClip;
    private SpriteRenderer _spriteRenderer;

    public AudioClip AttackClip
    {
        get
        {
            return _attackClip;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this._audioSource = GetComponent<AudioSource>();
        this._spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Death()
    {
        this._spriteRenderer.enabled = false;
        this._audioSource.clip = this._deathClip;
        this._audioSource.Play();
        Destroy(gameObject, 0.5f);
    }
}
