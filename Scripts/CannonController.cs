using UnityEngine;

public class CannonController : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float launchAcceleration = 10f;
    public float initialVelocity = 5f;

    AudioSource m_cannonSound;

    void Start()
    {
        m_cannonSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            FireProjectile();
            
            m_cannonSound.Play();
        }
    }

    void FireProjectile()
    {
        if (projectilePrefab == null || projectileSpawnPoint == null)
        {
            Debug.LogError("Missing projectilePrefab or projectileSpawnPoint!");
            return;
        }

        float spawnOffset = 0.6f;
        Vector3 spawnPos = projectileSpawnPoint.position + projectileSpawnPoint.forward * spawnOffset;
        Quaternion spawnRot = projectileSpawnPoint.rotation;

        GameObject projectile = Instantiate(projectilePrefab, spawnPos, spawnRot);

        
        var bulletComponent = projectile.GetComponent<OneShotProjectile>();
        if (bulletComponent != null)
            bulletComponent.SetOwner(gameObject);

        Collider bulletCol = projectile.GetComponent<Collider>();
        Collider cannonCol = GetComponent<Collider>();
        if (bulletCol != null && cannonCol != null)
        {
            Physics.IgnoreCollision(bulletCol, cannonCol);
        }

        if (bulletCol != null)
        {
            foreach (var childCollider in GetComponentsInChildren<Collider>())
            {
                Physics.IgnoreCollision(bulletCol, childCollider);
            }
        }

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            float v = initialVelocity + launchAcceleration * 1f;
            Vector3 launchDir = projectileSpawnPoint.forward * v;
            rb.velocity = launchDir;
        }
    }
}
