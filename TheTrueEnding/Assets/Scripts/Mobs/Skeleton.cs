using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField]
    private GameObject _arrowPrefab;
    [SerializeField]
    private Transform _firePoint;
    [SerializeField]
    private Transform _knight;
    [SerializeField]
    private float _attackRange = 2f;
    [SerializeField]
    private float _fireRate = 2f;
    private float _nextFireTime = 0f;

    private void Update()
    {
        float distanceToKnight = Vector2.Distance(transform.position, this._knight.position);

        if (distanceToKnight <= this._attackRange 
            && Time.time >= this._nextFireTime)
        {
            ShootArrow();
            this._nextFireTime = Time.time + this._fireRate;
        }
    }

    private void ShootArrow()
    {
        GameObject arrow = Instantiate(this._arrowPrefab, this._firePoint.position, this._firePoint.rotation);

        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            arrowScript.SetTarget(this._knight.transform.position);
        }
    }
}
