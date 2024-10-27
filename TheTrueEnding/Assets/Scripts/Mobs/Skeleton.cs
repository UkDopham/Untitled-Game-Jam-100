using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField]
    private GameObject _arrowPrefab;
    [SerializeField]
    private Transform _knight;
    [SerializeField]
    private float _attackRange = 2f;
    [SerializeField]
    private float _fireRate = 2f;
    private float _nextFireTime = 0f;
    private Transform _firePoint;
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _attackClip;
    [SerializeField]
    private AudioClip _deathClip;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        this._firePoint = GetComponent<Transform>();
        this._audioSource = GetComponent<AudioSource>();
        this._spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        try
        {
            float distanceToKnight = Vector2.Distance(transform.position, this._knight.position);

            if (distanceToKnight <= this._attackRange
                && Time.time >= this._nextFireTime)
            {
                ShootArrow();
                this._nextFireTime = Time.time + this._fireRate;
            }
        }
        catch (System.Exception e)
        {
        }
    }

    private void ShootArrow()
    {
        GameObject arrow = Instantiate(this._arrowPrefab, this._firePoint.position, this._firePoint.rotation);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            this._audioSource.clip = this._attackClip;
            this._audioSource.Play();
            arrowScript.SetTarget(this._knight.transform.position);
        }
    }
    public void Death()
    {
        this._spriteRenderer.enabled = false;
        this._audioSource.clip = this._deathClip;
        this._audioSource.Play();
        Destroy(gameObject, 0.5f);
    }
}
