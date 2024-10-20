using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public GameObject arrowPrefab;  // Arrow prefab to be instantiated
    public Transform firePoint;     // The point from which the arrow is shot
    public Transform knight;        // Reference to the Knight's position
    public float attackRange = 2f; // Range at which the skeleton can shoot the arrow
    public float fireRate = 2f;     // Time between each arrow shot
    private float nextFireTime = 0f; // Time control for next shot

    private void Update()
    {
        // Check if the knight is within range
        float distanceToKnight = Vector2.Distance(transform.position, knight.position);

        if (distanceToKnight <= attackRange && Time.time >= nextFireTime)
        {
            // If the knight is within range, shoot an arrow
            ShootArrow();
            nextFireTime = Time.time + fireRate;  // Set the next fire time
        }
    }

    private void ShootArrow()
    {
        // Create the arrow at the fire point
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);

        // Get the Arrow script and set the knight as the target
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if (arrowScript != null)
        {
            arrowScript.SetTarget(knight.transform.position);  // Assign the knight as the arrow's target
        }
    }
}
