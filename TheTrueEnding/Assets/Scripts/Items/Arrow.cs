using UnityEngine;
using UnityEngine.Tilemaps;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;           
    private Vector3 _direction;        

    public void SetTarget(Vector3 targetPosition)
    {
        this._direction = (targetPosition - transform.position).normalized;
    }

    private void Update()
    {
        transform.Translate(this._direction * this._speed * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Knight>() != null)
        {
            Knight knight = other.GetComponent<Knight>();
            if (knight != null)
            {
                knight.Hurt();
            }
            Destroy(gameObject);
        }
        else if (other.GetComponent<TilemapCollider2D>() != null)
        {
            Destroy(gameObject);
        }
    }
}
