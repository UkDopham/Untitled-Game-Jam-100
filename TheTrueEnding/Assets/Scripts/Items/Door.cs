using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    [SerializeField]
    private bool _isOpen;
    [SerializeField]
    private Tilemap _tilemap;
    private Animator _animator;
    [SerializeField]
    private KnightMovement _knightMovement;
    private AudioSource _audioSource;
    private Light2D _light2D;

    public bool isOpen
    {
        get
        {
            return _isOpen;
        }
    }
    private void Awake()
    {
        this._animator = GetComponent<Animator>();
        this._audioSource = GetComponent<AudioSource>();
        this._light2D = GetComponentInChildren<Light2D>();
    }
    private void OnMouseDown()
    {
        this._isOpen = !this._isOpen;
        this._animator.SetBool("isOpen", this._isOpen);
        this._audioSource.Play();
        this._light2D.enabled = !this._isOpen;
        this._knightMovement.MoveToNearestPoint();
    }
    public bool IsOnSameTile(Vector3Int vector)
    {
        Vector3 doorWorldPosition = transform.position;

        Vector3Int doorTilePosition = this._tilemap.WorldToCell(doorWorldPosition);

        return doorTilePosition == vector;
    }
}
