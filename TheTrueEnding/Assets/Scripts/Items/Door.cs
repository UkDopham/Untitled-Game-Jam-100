using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private bool _isOpen;
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
}
