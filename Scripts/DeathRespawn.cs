using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class DeathRespawn : MonoBehaviour
{
    [Header("Respawn")]
    public Transform respawnPoint;          
    public float invulnerableTime = 0.75f;  

    CharacterController controller;
    bool canBeHit = true;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!canBeHit) return;

        if (hit.collider.CompareTag("Enemy"))
        {
            Respawn();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!canBeHit) return;

        if (collision.collider.CompareTag("Enemy"))
        {
            Respawn();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!canBeHit) return;

        if (other.CompareTag("Enemy"))
        {
            Respawn();
        }
    }

    void Respawn()
    {
        if (respawnPoint == null)
        {
            Debug.LogWarning("Respawn Point not assigned in Inspector!");
            return;
        }

        StartCoroutine(DoRespawn());
    }

    System.Collections.IEnumerator DoRespawn()
    {
        canBeHit = false;

        controller.enabled = false;
        transform.position = respawnPoint.position;
        controller.enabled = true;

        if (invulnerableTime > 0f)
            yield return new WaitForSeconds(invulnerableTime);

        canBeHit = true;
    }
}
