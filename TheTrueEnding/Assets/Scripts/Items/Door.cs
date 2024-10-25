using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    [SerializeField]
    private bool _isOpen;
    [SerializeField]
    private Tilemap _tilemap;
    private Animator _animator;

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
    }
    private void OnMouseDown()
    {
        this._isOpen = !this._isOpen;
        this._animator.SetBool("isOpen", this._isOpen);
    }
    public bool IsOnSameTile(Vector3Int vector)
    {
        Vector3 doorWorldPosition = transform.position;

        Vector3Int doorTilePosition = this._tilemap.WorldToCell(doorWorldPosition);

        return doorTilePosition == vector;
    }
}
