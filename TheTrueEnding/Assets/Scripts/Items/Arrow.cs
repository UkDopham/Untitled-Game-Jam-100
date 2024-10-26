using UnityEngine;
using UnityEngine.Tilemaps;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    private Vector3 _direction;

    public void SetTarget(Vector3 targetPosition)
    {
        this._direction = (targetPosition - this.transform.position).normalized;
        float angle = Mathf.Atan2(this._direction.y, this._direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    private void Update()
    {
        this.transform.Translate(this._direction * this._speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Knight>() != null)
        {
            Knight knight = other.GetComponent<Knight>();
            if (knight != null)
            {
                knight.Hit();
            }
            Destroy(this.gameObject);
        }
        else if (other.GetComponent<TilemapCollider2D>() != null)
        {
            Destroy(this.gameObject);
        }
    }
}
