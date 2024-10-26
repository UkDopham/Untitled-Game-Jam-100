using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] 
    private Transform _target;
    [SerializeField]
    private Vector3 _offset;
    [SerializeField] 
    private float _smoothSpeed = 0.125f;

    void Start()
    {
        if (_offset == Vector3.zero && _target != null)
        {
            _offset = transform.position - _target.position;
        }
    }

    void LateUpdate()
    {
        if (_target != null)
        {
            Vector3 desiredPosition = new Vector3(_target.position.x + _offset.x, _target.position.y + _offset.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
